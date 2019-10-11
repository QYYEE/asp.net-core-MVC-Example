using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DD.Electricity.Cloud.AuthServer.Models
{
    public class UserEditViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Mobile { get; set; }
        public int ProjectId { get; set; }

    }
}
