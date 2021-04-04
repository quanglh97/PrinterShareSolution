using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace PrintShareSolution.ViewModels.System.Users
{
    public class RegisterRequest
    {
        [Display(Name = "Tên đầy đủ")]
        public string FullName { get; set; }

        [Display(Name = "Hòm thư")]
        public string Email { get; set; }

/*        [Display(Name = "Số điện thoại")]
        public string PhoneNumber { get; set; }*/

/*        [Display(Name = "Tài khoản")]
        public string UserName { get; set; }*/

/*        [Display(Name = "Mật khẩu")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Display(Name = "Xác nhận mật khẩu")]
        [DataType(DataType.Password)]
        public string ConfirmPassword { get; set; }*/
    }
}