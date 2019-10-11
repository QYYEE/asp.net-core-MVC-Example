using DD.Electricity.Cloud.AuthServer.Models;
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
    public class UserController : Controller
    {
        private HdDbContext _hdDbContext;
        public UserController(HdDbContext hdDbContext)
        {
            _hdDbContext = hdDbContext;
        }

        [HttpGet]
        public IActionResult Index()
        {
            var users = _hdDbContext.RegisteQueries.Where(t => true).Select(t => new UserViewModel
            {
                Id = t.Id,
                Name = t.Name,
                Mobile = t.Mobile,
                ProjectId = t.ProjectId

            }).ToList();
            return View(users);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Mobile,Description,IsPass,SubmitTime")] RegisteQuery registeQuery)
        {
            if(ModelState.IsValid)
            {
                _hdDbContext.Add(registeQuery);
                await _hdDbContext.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(registeQuery);
        }

        public IActionResult Edit(int id)
        {
            var users = _hdDbContext.RegisteQueries.FirstOrDefault(t => t.Id == id);
            var model = new UserEditViewModel()
            {
                Id = users.Id,
                Name = users.Name,
                Mobile = users.Mobile,
                ProjectId = users.ProjectId

            };
            return View(model);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, UserEditViewModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var users = _hdDbContext.RegisteQueries.FirstOrDefault(t => t.Id == id);
                    users.Name = model.Name;
                    _hdDbContext.RegisteQueries.Update(users);
                    _hdDbContext.SaveChanges();
                }

            }
            catch (Exception ex)
            {
                throw ex;

            }
            return RedirectToAction("Index");
        }
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var users = await _hdDbContext.RegisteQueries.FirstOrDefaultAsync(m => m.Id == id);
            if (users == null)
            {
                return NotFound();
            }
            return View(users);
        }

        public async Task<IActionResult> Delete(int?id)
        {
            if(id==null)
            {
                return NotFound();
            }
            var users = await _hdDbContext.RegisteQueries.FirstOrDefaultAsync(m => m.Id == id);
            if(users==null)
            {
                return NotFound();
            }
            return View(users);
        }

        [HttpPost,ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult>DeleteConfirmed(int id)
        {
            var users = await _hdDbContext.RegisteQueries.FindAsync(id);
            _hdDbContext.RegisteQueries.Remove(users);
            await _hdDbContext.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
     }       
}
