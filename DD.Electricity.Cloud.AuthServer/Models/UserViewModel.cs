using DD.Electricity.Cloud.Domain.Entities.Headquarter;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DD.Electricity.Cloud.AuthServer.Models
{
    public class UserViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Mobile { get; set; }
        public int ProjectId { get; set; }
        public List<RegisteQuery> RegisteQueries { get; set; }



    }
}
