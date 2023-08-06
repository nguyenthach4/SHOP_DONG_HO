using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebApp.Models
{
    public class LoginViewModel
    {
        [Required(ErrorMessage = "Bạn cần phải nhập tài khoản")]
        public string UserName { set; get; }
        [Required(ErrorMessage = "Bạn cần phải nhập mật khẩu")]
        public string Password { set; get; }
        public bool RememberMe { set; get; }
    }
}