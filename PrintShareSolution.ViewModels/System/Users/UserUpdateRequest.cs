using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace PrintShareSolution.ViewModels.System.Users
{
    public class UserUpdateRequest
    {
        /*[Display(Name = "MyId")]
        public string MyId { get; set; }*/

        [Display(Name = "Tên đầy đủ")]
        public string FullName { get; set; }

        [Display(Name = "Hòm thư")]
        public string Email { get; set; }

        [Display(Name = "Số điện thoại")]
        public string PhoneNumber { get; set; }
    }
}