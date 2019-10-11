using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace DD.Electricity.Cloud.AuthServer.Models.DDAccount
{
    public class DDEditUserViewModel:DDCreateUserViewModel
    {
        public string Id { get; set; }
        //[Required(ErrorMessage = "更改后密码必填")]
        //[Display(Name = "新密码")]
        //[MaxLength(16, ErrorMessage = "密码最大长度16")]

        //public string NewPassword { get; set; }

    }
}
