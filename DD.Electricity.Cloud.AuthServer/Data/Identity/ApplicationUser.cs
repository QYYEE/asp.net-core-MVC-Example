using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DD.Electricity.Cloud.AuthServer.Data.Identity
{
    public class ApplicationUser : IdentityUser
    {
        /// <summary>
        /// 用户可能有多个project的权限
        /// </summary>
        public string ProjectIds { get; set; } 
        public string GroupId { get; set; }
    }
}
