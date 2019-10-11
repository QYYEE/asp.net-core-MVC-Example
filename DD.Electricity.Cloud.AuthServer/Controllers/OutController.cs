using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DD.Electricity.Cloud.AuthServer.Data;
using DD.Electricity.Cloud.AuthServer.Data.Identity;
using DD.Electricity.Cloud.AuthServer.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace DD.Electricity.Cloud.AuthServer.Controllers
{
    [Route("api/out")]
    public class OutController : Controller
    {
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ApplicationDbContext _context;

        public OutController(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, ApplicationDbContext context)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _context = context;
        }

        /// <summary>
        /// 获取用户信息
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("userinfo")]
        public async Task<IActionResult> GetUserInfo(string name)
        {
            try
            {
                var result = new UserDto();
                //var user = await _userManager.FindByIdAsync(subjectId);
                var user = await _userManager.FindByNameAsync(name);
                result.UserName = user.UserName;
                return Json(result);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        /// <summary>
        /// 修改密码
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("resetpwd")]
        public async Task<IActionResult> ChangePassword(string name, string newPwd) 
        {
            var result = new ResultDto();

            var user = await _context.Users.FirstOrDefaultAsync(t => t.UserName == name);
            if (user != null)
            {
                //修改密码
                user.PasswordHash = _userManager.PasswordHasher.HashPassword(user, newPwd);

                _context.Users.Update(user);
                int n = _context.SaveChanges();
                if (n>0)
                {
                    result.Success = true;
                    result.Message = "修改密码成功";
                }
                else
                {
                    result.Success = false;
                    result.Message = "修改密码失败";
                }      
            }
            else
            {
                result.Success = false;
                result.Message = "用户不存在";
            }

            return Json(result);
        }

        /// <summary>
        /// 登出
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("signout")]
        public async Task<IActionResult> SignOut()
        {
            var result = new ResultDto();

            await _signInManager.SignOutAsync();

            result.Success = true;
            result.Message = "登出成功";

            return Json(result);
        }
    }
}
