using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace DD.Electricity.Cloud.AuthServer.Models.DDAccount
{
    public class DDCreateUserViewModel
    {
        [Required(ErrorMessage = "用户名必填")]
        [Display(Name = "用户名")]
        [MaxLength(16, ErrorMessage = "用户名最大长度16")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "密码必填")]
        [Display(Name = "密码")]
        [MaxLength(16, ErrorMessage = "密码最大长度16")]
        public string Password { get; set; }

        /// <summary>
        /// 1,2 
        /// </summary>
        public string ProjectId { get; set; }

        [Display(Name = "集团名称")]
        public string GroupId { get; set; }
        public List<SelectListItem> Groups { get; set; } = new List<SelectListItem>(); 

        public List<DDProjectCheckViewModel> Projects { get; set; } = new List<DDProjectCheckViewModel>();
    }
}
