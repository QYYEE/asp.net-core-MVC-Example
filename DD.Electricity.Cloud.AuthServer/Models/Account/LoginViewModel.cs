using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace DD.Electricity.Cloud.AuthServer.Models.Account
{
    public class LoginViewModel
    {
        [Display(Name = "用户名")]
        [Required(ErrorMessage ="用户名必填")]
        [MaxLength(16, ErrorMessage ="用户名长度不能超过16")]
        public string UserName { get; set; }

        [Display(Name = "密码")]
        [Required(ErrorMessage = "密码必填")]
        [MaxLength(16, ErrorMessage = "密码长度不能超过16")]
        public string Passowrd { get; set; }
    }
}
