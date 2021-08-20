using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace PrintShareSolution.ViewModels.System.Users
{
    public class UserVm
    {
        public Guid Id { get; set; }

        [Display(Name = "Tên đầy đủ")]
        public string FullName { get; set; }

        [Display(Name = "Số điện thoại")]
        public string PhoneNumber { get; set; }

        [Display(Name = "Tài khoản")]
        public string myId { get; set; }

        [Display(Name = "Email")]
        public string Email { get; set; }

        [Display(Name = "Phiên bản hiện tại")]
        public string CurrentVersion { get; set; }

        public IList<string> Roles { get; set; }
    }
}