using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DD.Electricity.Cloud.AuthServer.Data;
using DD.Electricity.Cloud.AuthServer.Data.Identity;
using DD.Electricity.Cloud.AuthServer.Models.DDAccount;
using DD.Electricity.Cloud.HdPersistence;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

namespace DD.Electricity.Cloud.AuthServer.Controllers
{
    /// <summary>
    /// 鼎鼎账户管理
    /// </summary>
    [Authorize]
    public class DDAcountController : Controller
    {
        private readonly HdDbContext _db;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<ApplicationRole> _roleManager;
        private readonly ApplicationDbContext _applicationDbContext;

        public DDAcountController(HdDbContext db, ApplicationDbContext applicationDbContext, UserManager<ApplicationUser> userManager, RoleManager<ApplicationRole> roleManager)
        {
            _applicationDbContext = applicationDbContext;
            _db = db;
            _userManager = userManager;
            _roleManager = roleManager;
        }
        public IActionResult Index()
        {
            var model = new List<DDIndexUserViewModel>();
            var users = _applicationDbContext.Users.Where(t => true).ToList();
            foreach(var user in users)
            {
                var groups = _db.Groups.FirstOrDefault(t => t.Id == int.Parse(user.GroupId));
                var item = new DDIndexUserViewModel();


                //_db.Projects.FirstOrDefault(t => t.Id == int.Parse(user.ProjectIds));
                //var dbProjectIds = new List<string>();

                string tempProjectNames = string.Empty;//存放拼接好的项目名称，以逗号分隔

                
                var projectIdLst = new List<string>(); //当前用户的选中项目主键集合
                if(!string.IsNullOrEmpty(user.ProjectIds))
                {
                    projectIdLst.AddRange(user.ProjectIds.Split(',').ToList());
                }

                var projectNameLst = new List<string>();//当前用户选中项目名称集合
                if(projectIdLst.Count>0)
                {
                    foreach(var projectId in projectIdLst)
                    {
                        var dbProject = _db.Projects.FirstOrDefault(t => t.Id == int.Parse(projectId));
                        projectNameLst.Add(dbProject.Name);
                    }
                }

                if(projectNameLst.Count>0)
                {
                    tempProjectNames = string.Join(',', projectNameLst);
                }
                
                item.ProjectName = tempProjectNames;


                item.Id = user.Id;
                //List<string> projectNames = new List<string>();
                //item.ProjectName = string.Join(",", projectNames.ToArray());
                item.UserName = user.UserName;
                item.GroupName = groups.Name;
                model.Add(item);
            }
            return View(model);
        }

        public IActionResult Create()
        {
            var model = new DDCreateUserViewModel();
            model.Groups = LoadGroups();
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(DDCreateUserViewModel model)
        {

            if(!ModelState.IsValid)
            {
                return View(model);
            }

            if(model.GroupId=="-1")
            {
                ModelState.AddModelError(string.Empty, "必须选择集团");
            }

            //创建用户
            var user = new ApplicationUser
            {
                UserName = model.UserName,
                GroupId = model.GroupId,
                ProjectIds = model.ProjectId
            };

            var createUserResult = await _userManager.CreateAsync(user, model.Password);
            if(!createUserResult.Succeeded)
            {
                ModelState.AddModelError(string.Empty, "创建用户失败");
            }

            //创建角色
            //默认用户的角色是 DianGong
            var createRoleResult = await _userManager.AddToRoleAsync(user, "DianGong");
            if(!createRoleResult.Succeeded)
            {
                ModelState.AddModelError(string.Empty, "创建用户角色失败");
            }

            if (ModelState.ErrorCount > 0)
            {
                return View(model);
            }


            return RedirectToAction(nameof(Index));
        }
        [HttpGet]
        public async Task<IActionResult> Edit(string id)
        {
            var model = new DDEditUserViewModel();

            var user = await _applicationDbContext.Users.FirstOrDefaultAsync(t => t.Id == id);
            //var users = await _userManager.GetUserAsync(User);
            //var groups = _db.Groups.FirstOrDefault(t => t.Id == int.Parse(users.GroupId));
            model.UserName = user.UserName;
            model.GroupId = user.GroupId;
            model.Groups = GroupByEdit(int.Parse(user.GroupId));
            model.Projects = GetProjectsbyGroupId(int.Parse(user.GroupId), user.ProjectIds);
            model.Password = user.PasswordHash;
            model.Id = user.Id;
            model.ProjectId = user.ProjectIds;
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(DDEditUserViewModel model)
        {
            //验证视图模型
            if(!ModelState.IsValid)
            {
                model.Groups= GroupByEdit(int.Parse(model.GroupId));
                model.Projects =GetProjectsbyGroupId(int.Parse(model.GroupId), model.ProjectId);
                return View(model);
            }

            //用户名重复不需要判断，因为界面已经禁止输入用户名
            //if(IsUserNameExitByEdit(model.UserName,int.Parse(model.Id))==false)
            //{
            //    ModelState.AddModelError(string.Empty, "用户名称已经存在");
            //}

            //找到用户
            var user = await _applicationDbContext.Users.FirstOrDefaultAsync(t => t.Id == model.Id);

            //var changePassword = await _userManager.ChangePasswordAsync(user,user.PasswordHash, model.Password);
            //if(!changePassword.Succeeded)
            //{
            //    ModelState.AddModelError(string.Empty, "修改密码失败");
            //}

            user.PasswordHash = _userManager.PasswordHasher.HashPassword(user, model.Password);
            user.GroupId = model.GroupId;
            user.ProjectIds = model.ProjectId;
            


            _applicationDbContext.Update(user);
            await _applicationDbContext.SaveChangesAsync();

            return RedirectToAction(nameof(Index));

        }
        private  bool IsUserNameExitByEdit(string UserName,int id)
        {
            var users = _applicationDbContext.Users.FirstOrDefault(t => int.Parse(t.Id) != id && t.UserName == UserName);
            return users == null ? true : false;
        }

        //public ActionResult GetProjectsByGroupId(string groupId, string userName="", string password="")
        //{
        //    var projects = _db.Projects.Where(t => t.GroupId == int.Parse(groupId));

        //    var tempProjects =new List<DDProjectCheckViewModel>();
        //    foreach(var project in projects)
        //    {
        //        var tempItem = new DDProjectCheckViewModel
        //        {
        //            Checked = false,
        //            Text = project.Name,
        //            Val = project.Id
        //        };

        //        tempProjects.Add(tempItem);
        //    }

        //    return RedirectToAction("Create", new { userName = userName, password = password, groupId = groupId, projects = tempProjects });
        //}
        private List<SelectListItem>GroupByEdit(int groupId)
        {
            var result = new List<SelectListItem>();
            result.Add(new SelectListItem { Text = "---选择集团---", Value = "-1", Selected = false });
            var dbGroup = _db.Groups.Where(t => true);
            foreach(var dbGroups in dbGroup)
            {
                var item = new SelectListItem();
                item.Text = dbGroups.Name;
                item.Value = dbGroups.Id.ToString();
                item.Selected = groupId == dbGroups.Id ? true : false;
                result.Add(item);
            }
            return result;
        }

        private List<SelectListItem> LoadGroups(string currentGroupId=null)
        {
            var result = new List<SelectListItem>();
            var dbGroups = _db.Groups.Where(t => true).ToList();
            if(string.IsNullOrEmpty(currentGroupId)) //创建用户显示集团
            {
                result.Add(new SelectListItem { Text = "---选择集团---", Value = "-1", Selected = true });
            }
            else //编辑用户显示集团
            {
                result.Add(new SelectListItem { Text = "---选择集团---", Value = "-1", Selected = false });
            }

            foreach(var group in dbGroups)
            {
                var item = new SelectListItem();
                item.Text = group.Name;
                item.Value = group.Id.ToString();
                item.Selected = !string.IsNullOrEmpty(currentGroupId) && currentGroupId == group.Id.ToString() ? true : false;

                result.Add(item);
            }
            return result;
        }

        /// <summary>
        /// 添加选择集团显示项目
        /// </summary>
        /// <param name="groupId"></param>
        /// <returns></returns>
        public IActionResult GetProjectsByGroup(string groupId)
        {
            List<DDProjectCheckViewModel> result = new List<DDProjectCheckViewModel>();
            var dbProjects = _db.Projects.Where(t => t.GroupId == int.Parse(groupId));
            foreach(var dbProject in dbProjects)
            {
                var item = new DDProjectCheckViewModel();
                item.Text = dbProject.Name;
                item.Val = dbProject.Id;

                result.Add(item);
            }

            //return Json(JsonConvert.SerializeObject(result));
            return View(result);
        }

        public IActionResult GetProjectsByGroupEdit(string groupId,string projectIds)
        {
            List<DDProjectCheckViewModel> result = new List<DDProjectCheckViewModel>();

            List<string> currentProjectIds = new List<string>();
            if(!string.IsNullOrEmpty(projectIds))
            {
                currentProjectIds.AddRange(projectIds.Split(',').ToList());
            }

            var dbProjects = _db.Projects.Where(t => t.GroupId == int.Parse(groupId));
            foreach (var dbProject in dbProjects)
            {
                var item = new DDProjectCheckViewModel();
                item.Text = dbProject.Name;
                item.Val = dbProject.Id;
                item.Checked = currentProjectIds.Any(t => t == dbProject.Id.ToString()) ? true : false;
                result.Add(item);
            }
            return View(result);
        }
       
        /// <summary>
        /// 根据GroupId和当前选中的ProjectId
        /// </summary>
        /// <param name="groupId"></param>
        /// <param name="projectIds">1,2</param>
        /// <returns></returns>
       private List<DDProjectCheckViewModel> GetProjectsbyGroupId(int groupId, string projectIds)
        {
            var dbProjectIds = new List<string>();
            if(!string.IsNullOrEmpty(projectIds))
            {
                dbProjectIds.AddRange(projectIds.Split(',').ToList());
            }

            var result = new List<DDProjectCheckViewModel>();
            var dbProjects = _db.Projects.Where(t => t.GroupId == groupId);
            foreach (var dbProject in dbProjects)
            {
                var item = new DDProjectCheckViewModel();
                item.Text = dbProject.Name;
                item.Val = dbProject.Id;
                item.Checked = dbProjectIds.Any(t => t == dbProject.Id.ToString()) ? true : false;

                result.Add(item);
            }
            return result;
        }  
    }
}