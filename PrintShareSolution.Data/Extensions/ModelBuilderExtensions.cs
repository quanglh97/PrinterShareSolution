
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using PrintShareSolution.Data.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace eShopSolution.Data.Extensions
{
    public static class ModelBuilderExtensions
    {
        public static void Seed(this ModelBuilder modelBuilder)
        {
            var roleId = new Guid("8D04DCE2-969A-435D-BBA4-DF3F325983DC");
            var adminId = new Guid("69BD714F-9576-45BA-B5B7-F00649BE00DE");
            var clientId = new Guid("69BD714F-9576-45BA-B5B7-F00649BE0044");

            modelBuilder.Entity<AppConfig>().HasData(
                new AppConfig() { Key = "HomeTitle", Value = "This is home page of PrinterShareSolution" },
                new AppConfig() { Key = "HomeKeyword", Value = "This is keyword of PrinterShareSolution" },
                new AppConfig() { Key = "HomeDescription", Value = "This is description of PrinterShareSolution" });

            modelBuilder.Entity<Printer>().HasData(
                new Printer() { Id = 1, Name = "P1", Status = PrintShareSolution.Data.Enums.Status.InActive},
                new Printer() { Id = 2, Name = "P2", Status = PrintShareSolution.Data.Enums.Status.InActive});

            modelBuilder.Entity<OrderPrintFile>().HasData(
                new OrderPrintFile() { Id = 1, UserId = adminId, PrinterId = 1, FileName = "xxx.docx", FilePath = "C://xxx.docx" },
                new OrderPrintFile() { Id = 2, UserId = adminId, PrinterId = 2, FileName = "xxx.docx", FilePath = "C://xxx.docx" });


            modelBuilder.Entity<ListPrinterOfUser>().HasData(
                new ListPrinterOfUser() { Id = 1, UserId = adminId, PrinterId = 1 },
                new ListPrinterOfUser() { Id = 2, UserId = adminId, PrinterId = 2 });

            modelBuilder.Entity<BlockList>().HasData(
                new BlockList() {Id = 1, UserId = adminId, UserBlockedId = clientId , BlackListFilePath = "C://BackList.txt"} );

            // any guid
            
            modelBuilder.Entity<AppRole>().HasData(new AppRole
            {
                Id = roleId,
                Name = "admin",
                NormalizedName = "admin",
                Description = "Administrator role"
            });

            var hasher = new PasswordHasher<AppUser>();
            modelBuilder.Entity<AppUser>().HasData(new AppUser
            {
                Id = adminId,
                UserName = "admin",
                NormalizedUserName = "admin",
                Email = "quanglehoi@gmail.com",
                NormalizedEmail = "quanglehoi@gmail.com",
                EmailConfirmed = true,
                PasswordHash = hasher.HashPassword(null, "Abcd1234$"),
                SecurityStamp = string.Empty,
                FullName = "Lê Hội Quang",
              
            });

            modelBuilder.Entity<IdentityUserRole<Guid>>().HasData(new IdentityUserRole<Guid>
            {
                RoleId = roleId,
                UserId = adminId
            });
        }
    }
}