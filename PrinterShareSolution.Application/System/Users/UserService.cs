﻿
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using PrinterShareSolution.Application.System.Users;
using PrintShareSolution.Data.EF;
using PrintShareSolution.Data.Entities;
using PrintShareSolution.ViewModels.Common;
using PrintShareSolution.ViewModels.System.Users;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace eShopSolution.Application.System.Users
{
    public class UserService : IUserService
    {
        private readonly PrinterShareDbContext _context;
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly RoleManager<AppRole> _roleManager;
        private readonly IConfiguration _config;

        private static Random random = new Random();

        public UserService(PrinterShareDbContext context,
            UserManager<AppUser> userManager,
            SignInManager<AppUser> signInManager,
            RoleManager<AppRole> roleManager,
            IConfiguration config)
        {
            _context = context;
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
            _config = config;
        }

        public async Task<ApiResult<string>> Authencate(LoginRequest request)
        {
            var user = await _userManager.FindByNameAsync(request.UserName);
            if (user == null) return new ApiErrorResult<string>("Tài khoản không tồn tại");

            var result = await _signInManager.PasswordSignInAsync(user, request.Password, request.RememberMe, true);
            if (!result.Succeeded)
            {
                return new ApiErrorResult<string>("Đăng nhập không đúng");
            }
            var roles = await _userManager.GetRolesAsync(user);
            var claims = new[]
            {
                new Claim(ClaimTypes.Email,user.Email),
                new Claim(ClaimTypes.GivenName,user.FullName),
                new Claim(ClaimTypes.Role, string.Join(";",roles)),
                new Claim(ClaimTypes.Name, request.UserName)
            };
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Tokens:Key"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(_config["Tokens:Issuer"],
                _config["Tokens:Issuer"],
                claims,
                expires: DateTime.Now.AddHours(3),
                signingCredentials: creds);

            return new ApiSuccessResult<string>(new JwtSecurityTokenHandler().WriteToken(token));
        }

        public async Task<ApiResult<bool>> Delete(string myId)
        {
            var user = await _userManager.FindByNameAsync(myId);
            if (user == null)
            {
                return new ApiErrorResult<bool>("User không tồn tại");
            }

            var printerOfUserQuery = from lpou in _context.ListPrinterOfUsers
                                     where lpou.UserId == user.Id
                                     select new { lpou };

            foreach(var index in printerOfUserQuery)
            {
                var Printer = await _context.Printers.FindAsync(index.lpou.PrinterId);
                if (Printer != null)
                {
                    _context.Printers.Remove(Printer);
                    _context.ListPrinterOfUsers.Remove(index.lpou);
                }
            }
            await _context.SaveChangesAsync();

            var result = await _userManager.DeleteAsync(user);
            if (result.Succeeded)
                return new ApiSuccessResult<bool>();

            return new ApiErrorResult<bool>("Xóa không thành công");
        }

        public async Task<ApiResult<UserVm>> GetById(string myId)
        {
            var user = await _userManager.FindByNameAsync(myId);
            if (user == null)
            {
                return new ApiErrorResult<UserVm>("User không tồn tại");
            }
            var roles = await _userManager.GetRolesAsync(user);
            var userVm = new UserVm()
            {
                Email = user.Email,
                PhoneNumber = user.PhoneNumber,      
                Id = user.Id,
                FullName = user.FullName,
                myId = user.UserName,
                Roles = roles
            };
            return new ApiSuccessResult<UserVm>(userVm);
        }

        public async Task<ApiResult<PagedResult<UserVm>>> GetUsersPaging(GetUserPagingRequest request)
        {
            var query = _userManager.Users;
            if (!string.IsNullOrEmpty(request.Keyword))
            {
                query = query.Where(x => x.UserName.Contains(request.Keyword)
                 || x.PhoneNumber.Contains(request.Keyword) || x.Email.Contains(request.Keyword));
            }

            //3. Paging
            int totalRow = await query.CountAsync();

            var data = await query.Skip((request.PageIndex - 1) * request.PageSize)
                .Take(request.PageSize)
                .Select(x => new UserVm()
                {
                    Email = x.Email,
                    PhoneNumber = x.PhoneNumber,
                    myId = x.UserName,
                    FullName = x.FullName,
                    Id = x.Id,
                    CurrentVersion = x.CurrentVersion
                }).ToListAsync();

            //4. Select and projection
            var pagedResult = new PagedResult<UserVm>()
            {
                TotalRecords = totalRow,
                PageIndex = request.PageIndex,
                PageSize = request.PageSize,
                Items = data
            };
            return new ApiSuccessResult<PagedResult<UserVm>>(pagedResult);
        }

        public async Task<ApiResult<UserVm>> Register(RegisterRequest request)
        {
            var User = await _userManager.FindByEmailAsync(request.Email);
            if (User != null)
            {
                if(User.FullName.CompareTo(request.FullName) == 0)
                {
                    var DataUser = new UserVm()
                    {
                        Email = User.Email,
                        PhoneNumber = User.PhoneNumber,
                        myId = User.UserName,
                        FullName = User.FullName,
                        Id = User.Id,
                    };
                    return new ApiSuccessResult<UserVm>(DataUser);
                }   
                else
                    return new ApiErrorResult<UserVm>("Emai đã tồn tại");
            }

            //request.UserName = RandomString(8);
            var userName = RandomString(6);
            var phoneNumber = RandomString(10);
            //var user = await _userManager.FindByNameAsync(request.UserName);
            var user = await _userManager.FindByNameAsync(userName);
            while (user != null)
            {
                //request.UserName = RandomString(8);
                userName = RandomString(8);
                user = await _userManager.FindByNameAsync(userName);
            } ;
            

            user = new AppUser()
            {
                Email = request.Email,
                FullName = request.FullName,
                UserName = userName, 
                PhoneNumber = phoneNumber
            };
            var result = await _userManager.CreateAsync(user, "Aa12345@");
            var userVm = new UserVm()
            {
                FullName = user.FullName,
                Email = user.Email,
                myId = user.UserName,
                PhoneNumber = user.PhoneNumber,
                Id = user.Id,
                CurrentVersion = user.CurrentVersion
            };
            if (result.Succeeded)
            {
                return new ApiSuccessResult< UserVm > (userVm);
            }
            return new ApiErrorResult<UserVm>("Đăng ký không thành công");
        }

        public async Task<ApiResult<bool>> RoleAssign(Guid id, RoleAssignRequest request)
        {
            var user = await _userManager.FindByIdAsync(id.ToString());
            if (user == null)
            {
                return new ApiErrorResult<bool>("Tài khoản không tồn tại");
            }
            var removedRoles = request.Roles.Where(x => x.Selected == false).Select(x => x.Name).ToList();
            foreach (var roleName in removedRoles)
            {
                if (await _userManager.IsInRoleAsync(user, roleName) == true)
                {
                    await _userManager.RemoveFromRoleAsync(user, roleName);
                }
            }
            await _userManager.RemoveFromRolesAsync(user, removedRoles);

            var addedRoles = request.Roles.Where(x => x.Selected).Select(x => x.Name).ToList();
            foreach (var roleName in addedRoles)
            {
                if (await _userManager.IsInRoleAsync(user, roleName) == false)
                {
                    await _userManager.AddToRoleAsync(user, roleName);
                }
            }

            return new ApiSuccessResult<bool>();
        }

        public async Task<ApiResult<bool>> Update(string myId, UserUpdateRequest request)
        {
            if (await _userManager.Users.AnyAsync(x => x.Email == request.Email && x.UserName != myId))
            {
                return new ApiErrorResult<bool>("Emai đã tồn tại");
            }
            var user = await _userManager.FindByNameAsync(myId);
            user.Email = request.Email;
            user.FullName = request.FullName;
            user.PhoneNumber = request.PhoneNumber;

            var result = await _userManager.UpdateAsync(user);
            if (result.Succeeded)
            {
                return new ApiSuccessResult<bool>();
            }
            return new ApiErrorResult<bool>("Cập nhật không thành công");
        }

        
        public static string RandomString(int length)
        {
            //const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            const string chars = "0123456789";
            return new string(Enumerable.Repeat(chars, length)
              .Select(s => s[random.Next(s.Length)]).ToArray());
        }
    }
}