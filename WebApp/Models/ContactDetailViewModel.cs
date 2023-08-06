using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebApp.Models
{
    public class ContactDetailViewModel
    {

        public int ID { set; get; }

        [Required(ErrorMessage = "Tên không được để trống")]
        public string Name { set; get; }


        [MaxLength(50,ErrorMessage = "Số điện thoại không được vượt quá 50 ký tự")]
        public string Phone { set; get; }

        [MaxLength(250, ErrorMessage = "Email không được vượt quá 250 ký tự")]
        public string Email { set; get; }

        [MaxLength(250, ErrorMessage = "Website không được vượt quá 250 ký tự")]
        public string Website { set; get; }

        [MaxLength(250, ErrorMessage = "Địa chỉ không được vượt quá 250 ký tự")]
        public string Address { set; get; }

        public string Other { set; get; }

        public double? Lat { set; get; }

        public double? Lng { set; get; }

        public bool Status { set; get; }
    }
}