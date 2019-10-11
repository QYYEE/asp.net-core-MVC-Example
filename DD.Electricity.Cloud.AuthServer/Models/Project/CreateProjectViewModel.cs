using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace DD.Electricity.Cloud.AuthServer.Models.Project
{
    public class CreateProjectViewModel
    {
        [Display(Name = "项目名称")]
        [Required(ErrorMessage = "项目名称必填")]
        [MaxLength(16, ErrorMessage = "项目名称最大长度不能超过16")]
        public string ProjectName { get; set; }

        [Display(Name = "集团")]
        [Required(ErrorMessage = "集团必填")]
        public string GroupId { get; set; }
        public List<SelectListItem> Items { get; set; } = new List<SelectListItem>();
    }
}
