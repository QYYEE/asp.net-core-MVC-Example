using DD.Electricity.Cloud.AuthServer.Models;
using DD.Electricity.Cloud.AuthServer.Models.Project;
using DD.Electricity.Cloud.Domain.Entities.Headquarter;
using DD.Electricity.Cloud.HdPersistence;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DD.Electricity.Cloud.AuthServer.Controllers
{
    public class ProjectController:Controller
    {
        private HdDbContext _hdDbContext;
        public ProjectController(HdDbContext hdDbContext)
        {
            _hdDbContext = hdDbContext;
        }
        [HttpGet]
        public IActionResult Index()
        {
            var model = new List<DisplayProjectViewModel>();

            var dbProjects = _hdDbContext.Projects.Where(t => true).ToList();
            foreach(var dbProject in dbProjects)
            {
                var item = new DisplayProjectViewModel();
                item.Id = dbProject.Id;
                item.ProjectName = dbProject.Name;

                var dbGroup = _hdDbContext.Groups.FirstOrDefault(t => t.Id == dbProject.GroupId);
                item.GroupName = dbGroup.Name;
                model.Add(item);
            }
            return View(model);     
        }
        public IActionResult Edit(int id)
        {
            var model = new EditProjectViewModel();
            var dbproject = _hdDbContext.Projects.FirstOrDefault(t => t.Id == id);
            model.Id = dbproject.Id;
            model.ProjectName = dbproject.Name;
            model.Items = PopulateProjectByEdit(dbproject.Id);
            return View(model);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id,EditProjectViewModel model)
        {
            if(!ModelState.IsValid)
            {
                model.Items = PopulateProjectByEdit(model.Id);
                return View(model);
            }
            if(IsProjectNameExitsByEdit(model.ProjectName,model.Id)==false)
            {
                ModelState.AddModelError(string.Empty, "项目名称已经存在");
            }
            if(ModelState.ErrorCount>0)
            {
                model.Items = PopulateProjectByEdit(model.Id);
                return View(model);
            }

            var dbProject = _hdDbContext.Projects.FirstOrDefault(t => t.Id == model.Id);
            var dbGroup = _hdDbContext.Groups.FirstOrDefault(t => t.Id == int.Parse(model.GroupId));

            dbProject.Name = model.ProjectName;
            dbProject.GroupId = dbGroup.Id;
            dbProject.Group = dbGroup;

            _hdDbContext.Update(dbProject);
            await _hdDbContext.SaveChangesAsync();

            return RedirectToAction(nameof(Index));

        }
        private bool IsProjectNameExitsByEdit(string projectName,int id)
        {
            var dbprojectt = _hdDbContext.Projects.FirstOrDefault(t => t.Id != id && t.Name == projectName);
            return dbprojectt == null ? true : false;
        }

        private List<SelectListItem>PopulateProjectByEdit(int GroupId)
        {
            var result = new List<SelectListItem>();
            result.Add(new SelectListItem { Text = "---选择集团---", Value = "-1", Selected = false });
            var dbGroup = _hdDbContext.Groups.Where(t => true);
            foreach(var dbGroups in dbGroup)
            {
                var item = new SelectListItem();
                item.Text = dbGroups.Name;
                item.Value = dbGroups.Id.ToString();
                item.Selected = GroupId == dbGroups.Id ? true : false;

                result.Add(item);
            }
            return result;

        }

        public IActionResult Create()
        {
            var model = new CreateProjectViewModel();
            model.Items = PopulateGroup();

            return View(model);
            
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(CreateProjectViewModel model)
        {
            if (!ModelState.IsValid)
            {
                model.Items = PopulateGroup();
                return View(model);
            }
            if(IsProjectNameExists(model.ProjectName)==false)
            {
                ModelState.AddModelError(string.Empty, "项目的名称已经存在");
            }
            if(ModelState.ErrorCount>0)
            {
                model.Items = PopulateGroup();
                return View(model);
            }
            var dbProject=new DD.Electricity.Cloud.Domain.Entities.Headquarter.Project();
            dbProject.Name = model.ProjectName;
            dbProject.GroupId = int.Parse(model.GroupId);

            _hdDbContext.Projects.Add(dbProject);
            _hdDbContext.SaveChanges();

            return RedirectToAction(nameof(Index));

        }
        private List<SelectListItem> PopulateGroup()
        {
            var result = new List<SelectListItem>();
            result.Add(new SelectListItem { Text = "---选择集团---", Value = "-1", Selected = true });
            result.AddRange(_hdDbContext.Groups.Where(t => true).Select(t => new SelectListItem { Text = t.Name, Value = t.Id.ToString() }).ToList());

            return result;
        }
        private bool IsProjectNameExists(string projectName)
        {
            var dbProject = _hdDbContext.Projects.FirstOrDefault(t => t.Name == projectName);
            return dbProject == null ? true : false;
        }

        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> Create([Bind("Id,Name,Description,ServerIp,Port,DbName,UserName,Password,Connectionkey,Projectlmgs,Provinceld,CityId,DistricId,Address,Lat,lot,JoinDate,IsActive,GroupId,Sort")] Project projects)
        //{
        //    if(ModelState.IsValid)
        //    {
        //        _hdDbContext.Add(projects);
        //        await _hdDbContext.SaveChangesAsync();
        //        return RedirectToAction(nameof(Index));
        //    }
        //    return View(projects);
        //}

        public async Task<IActionResult> Details(int?id)
        {
            if(id==null)
            {
                return NotFound();
            }
            var projects = await _hdDbContext.Projects.FirstOrDefaultAsync(m => m.Id == id);
            if(projects==null)
            {
                return NotFound();
            }
            return View(projects);
        }


        public async Task<IActionResult> Delete(int?id)
        {
            if(id==null)
            {
                return NotFound();
            }
            var projects = await _hdDbContext.Projects
                .FirstOrDefaultAsync(m => m.Id == id);
            if(projects==null)
            {
                return NotFound();
            }
            return View(projects);
        }


        [HttpPost,ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var projects = await _hdDbContext.Projects.FindAsync(id);
            _hdDbContext.Projects.Remove(projects);
            await _hdDbContext.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}
