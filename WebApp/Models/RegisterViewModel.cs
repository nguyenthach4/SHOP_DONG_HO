using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebApp.Models
{
    public class RegisterViewModel
    {
        [Required(ErrorMessage = "Bạn cần phải nhập tên")]
        public string FullName { set; get; }
        [Required(ErrorMessage = "Bạn cần phải nhập tên đăng nhập")]
        public string UserName { set; get; }
        [Required(ErrorMessage = "Bạn cần phải nhập mật khẩu")]
        [MinLength(6, ErrorMessage = "Mật khẩu phải có ít nhất 6 ký tự")]
        public string Password { set; get; }
        [Required(ErrorMessage = "Bạn cần phải nhập Email")]
        [EmailAddress(ErrorMessage = "Địa chỉ Email không đúng")]
        public string Email { set; get; }
        public string Address { set; get; }
        [Required(ErrorMessage = "Bạn cần phải nhập số điện thoại")]
        public string PhoneNumber { set; get; }
    }
}