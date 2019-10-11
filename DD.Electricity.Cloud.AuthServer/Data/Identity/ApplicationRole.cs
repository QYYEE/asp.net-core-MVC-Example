using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DD.Electricity.Cloud.AuthServer.Data.Identity
{
    public class ApplicationRole : IdentityRole
    {
        public string ProjectIds { get; set; }
        public string GroupId { get; set; }
    }
}
