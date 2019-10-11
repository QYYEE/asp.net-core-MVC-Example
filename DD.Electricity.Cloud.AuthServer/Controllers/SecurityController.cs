using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DD.Electricity.Cloud.AuthServer.Data.Identity;
using DD.Electricity.Cloud.AuthServer.Models.Account;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace DD.Electricity.Cloud.AuthServer.Controllers
{
    [AllowAnonymous]
    public class SecurityController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;

        public SecurityController(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if(!ModelState.IsValid)
            {
                return View(model);
            }

            //查找用户
            var user = await _userManager.FindByNameAsync(model.UserName);
            if(user==null)
            {
                ModelState.AddModelError(string.Empty, "用户名不存在");
                return View(model);
            }

            //登录
            var result = await _signInManager.PasswordSignInAsync(model.UserName, model.Passowrd, false, false);
            if(!result.Succeeded)
            {
                ModelState.AddModelError(string.Empty, "密码不正确");
            }

            return RedirectToAction("Index", "Home");
        }

        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Login");
        }

        public IActionResult AccessDenied()
        {
            return View();
        }
    }
}