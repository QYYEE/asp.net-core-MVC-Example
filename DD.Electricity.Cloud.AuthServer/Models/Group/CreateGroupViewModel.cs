using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace DD.Electricity.Cloud.AuthServer.Models.Group
{
    public class CreateGroupViewModel
    {
        [Display(Name ="集团名称")]
        [Required(ErrorMessage ="集团名称必填")]
        [MaxLength(16, ErrorMessage ="集团名称最大长度不能超过16")]
        public string GroupName { get; set; }

        [Display(Name = "行业")]
        [Required(ErrorMessage = "行业必填")]
        public string IndustryId { get; set; }
        public List<SelectListItem> Items { get; set; } = new List<SelectListItem>();
    }
}
