using DD.Electricity.Cloud.AuthServer.Models;
using DD.Electricity.Cloud.AuthServer.Models.Group;
using DD.Electricity.Cloud.Domain.Entities.Headquarter;
using DD.Electricity.Cloud.HdPersistence;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace DD.Electricity.Cloud.AuthServer.Controllers
{
    [Authorize]
    public class GroupController : Controller
    {
        private HdDbContext _hdDbContext;
        public GroupController(HdDbContext hdDbContext)
        {
            _hdDbContext = hdDbContext;
        }

        [HttpGet]
        public IActionResult Index()
        {
            var model = new List<DisplayGroupViewModel>();

            var dbGroups = _hdDbContext.Groups.Where(t => true).ToList();
            foreach (var dbGroup in dbGroups)
            {
                var item = new DisplayGroupViewModel();
                item.GroupName = dbGroup.Name;
                item.Id = dbGroup.Id;

                var dbIndustry = _hdDbContext.Industries.FirstOrDefault(t => t.Id == dbGroup.IndustryId);
                item.IndustryName = dbIndustry.Name;
                model.Add(item);

            }

            return View(model);
        }

        public IActionResult Create()
        {
            var model = new CreateGroupViewModel();
            model.Items = PopulateIndestries();


            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(CreateGroupViewModel model)
        {
            if (!ModelState.IsValid)
            {
                model.Items = PopulateIndestries();
                return View(model);
            }

            if (model.IndustryId == "-1")
            {
                ModelState.AddModelError(string.Empty, "必须选择行业");
            }

            if(IsGroupNameExists(model.GroupName) ==false)
            {
                ModelState.AddModelError(string.Empty, "集团名称已经存在");
            }

            if (ModelState.ErrorCount > 0)
            {
                model.Items = PopulateIndestries();
                return View(model);
            }

            var dbGroup = new DD.Electricity.Cloud.Domain.Entities.Headquarter.Group();
            dbGroup.Name = model.GroupName;
            dbGroup.IndustryId = int.Parse(model.IndustryId);

            _hdDbContext.Groups.Add(dbGroup);
            _hdDbContext.SaveChanges();

            return RedirectToAction(nameof(Index));
        }

        /// <summary>
        /// 名称已经存在返回false
        /// </summary>
        /// <param name="groupName"></param>
        /// <returns></returns>
        private bool IsGroupNameExists(string groupName)
        {
            var dbGroup = _hdDbContext.Groups.FirstOrDefault(t => t.Name == groupName);
            return dbGroup == null ? true : false;
        }

        /// <summary>
        /// 存在返回false
        /// </summary>
        private bool IsGroupNameExistsByEdit(string groupName, int id)
        {
            var dbGroup = _hdDbContext.Groups.FirstOrDefault(t => t.Id != id && t.Name == groupName);
            return dbGroup == null ? true : false;
        }

        private List<SelectListItem> PopulateIndestries()
        {
            var result = new List<SelectListItem>();
            result.Add(new SelectListItem { Text = "---选择行业---", Value = "-1", Selected = true });
            result.AddRange(_hdDbContext.Industries.Where(t => t.ParentId != "-1").Select(t => new SelectListItem { Text = t.Name, Value = t.Id.ToString() }).ToList());

            return result;
        }

        private List<SelectListItem> PopulateIndustriesByEdit(int industryId)
        {
            var result = new List<SelectListItem>();
            result.Add(new SelectListItem { Text = "---选择行业---", Value = "-1", Selected = false });

            var dbIndustries = _hdDbContext.Industries.Where(t => t.ParentId != "-1");
            foreach (var dbIndustry in dbIndustries)
            {
                var item = new SelectListItem();
                item.Text = dbIndustry.Name;
                item.Value = dbIndustry.Id.ToString();
                item.Selected = industryId == dbIndustry.Id ? true : false;

                result.Add(item);
            }

            return result;
        }


        public IActionResult Edit(int id)
        {
            var model = new EditGroupViewModel();

            var dbGroup = _hdDbContext.Groups.FirstOrDefault(t => t.Id == id);
            model.Id = dbGroup.Id;
            model.GroupName = dbGroup.Name;
            model.Items = PopulateIndustriesByEdit(dbGroup.IndustryId);
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(EditGroupViewModel editGroupViewModel)
        {
            if (!ModelState.IsValid)
            {
                editGroupViewModel.Items = PopulateIndustriesByEdit(editGroupViewModel.Id);
                return View(editGroupViewModel);
            }

            if (editGroupViewModel.IndustryId == "-1")
            {
                ModelState.AddModelError(string.Empty, "必须选择行业");
            }

            if(IsGroupNameExistsByEdit(editGroupViewModel.GroupName, editGroupViewModel.Id)==false)
            {
                ModelState.AddModelError(string.Empty, "集团名称已经存在");
            }

            if(ModelState.ErrorCount>0)
            {
                editGroupViewModel.Items = PopulateIndustriesByEdit(editGroupViewModel.Id);
                return View(editGroupViewModel);
            }


            var dbGroup = _hdDbContext.Groups.FirstOrDefault(t => t.Id == editGroupViewModel.Id);
            var dbIndustry = _hdDbContext.Industries.FirstOrDefault(t => t.Id == int.Parse(editGroupViewModel.IndustryId));

            dbGroup.Name = editGroupViewModel.GroupName;
            dbGroup.IndustryId = dbIndustry.Id;
            dbGroup.Industry = dbIndustry;

            _hdDbContext.Update(dbGroup);
            await _hdDbContext.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        //public async Task<IActionResult> Details(int? id)
        //{
        //    if (id == null)
        //    {
        //        return NotFound();
        //    }
        //    var groups = await _hdDbContext.Groups.FirstOrDefaultAsync(m => m.Id == id);
        //    if (groups == null)
        //    {
        //        return NotFound();
        //    }
        //    return View(groups);
        //}
    }
}




//[HttpGet]
//public IActionResult Edit(int? id)
//{
//    if(id==null)
//    {
//        return NotFound();
//    }

//    var groups = _hdDbContext.Groups.FirstOrDefault(t => t.Id == id);
//    var model = new GroupEditViewModel()
//    {
//        Id = groups.Id,
//        Name = groups.Name
//    };
//    return View(model);
//}
//[HttpPost]
//[ValidateAntiForgeryToken]
//public async Task<IActionResult> Edit(int id,[Bind("Id,Name")]GroupEditViewModel groupEditViewModel)
//{
//    if(id!=groupEditViewModel.Id)
//    {
//        return NotFound();
//    }
//    if(ModelState.IsValid)
//    {
//        try
//        {
//            _hdDbContext.Update(groupEditViewModel);
//            await _hdDbContext.SaveChangesAsync();

//        }
//        catch(DbUpdateConcurrencyException)
//        {
//            if(!GroupExists(groupEditViewModel.Id))
//            {
//                return NotFound();
//            }
//            else
//            {
//                throw;
//            }
//        }
//        return RedirectToAction(nameof(Index));
//    }
//    return View(groupEditViewModel);
//}


//        public async Task<IActionResult> Delete(int? id)
//        {
//            if (id == null)
//            {
//                return NotFound();
//            }

//            var groups = await _hdDbContext.Groups
//                .FirstOrDefaultAsync(m => m.Id == id);
//            if (groups == null)
//            {
//                return NotFound();
//            }

//            return View(groups);
//        }

//        // POST: Movices/Delete/5
//        [HttpPost, ActionName("Delete")]
//        [ValidateAntiForgeryToken]
//        public async Task<IActionResult> DeleteConfirmed(int id)
//        {
//            var groups = await _hdDbContext.Groups.FindAsync(id);
//            _hdDbContext.Groups.Remove(groups);
//            await  _hdDbContext.SaveChangesAsync();
//            return RedirectToAction(nameof(Index));
//        }
//        private bool GroupExists(int id)
//        {
//            return _hdDbContext.Groups.Any(e => e.Id == id);
//        }
//    }
//}
