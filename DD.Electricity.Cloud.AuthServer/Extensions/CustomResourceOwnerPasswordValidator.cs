using DD.Electricity.Cloud.AuthServer.Data.Identity;
using IdentityModel;
using IdentityServer4.Models;
using IdentityServer4.Validation;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace DD.Electricity.Cloud.AuthServer.Extensions
{
    public class CustomResourceOwnerPasswordValidator : IResourceOwnerPasswordValidator
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly IUserStore<ApplicationUser> _userStore;

        public CustomResourceOwnerPasswordValidator(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, IUserStore<ApplicationUser> userStore)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _userStore = userStore;
        }
        public async Task ValidateAsync(ResourceOwnerPasswordValidationContext context)
        {
            try
            {
                var user =await _userManager.FindByNameAsync(context.UserName);
                if(user!=null)
                {
                    if(user.PasswordHash ==_userManager.PasswordHasher.HashPassword(user, context.Password))
                    {
                        context.Result = new GrantValidationResult(user.Id, "custom", GetUserClaims(user));
                        return;
                    }
                    context.Result = new GrantValidationResult(TokenRequestErrors.InvalidGrant, "密码不正确");
                    return;
                }
                context.Result = new GrantValidationResult(TokenRequestErrors.InvalidGrant, "用户不存在");
                return;
            }
            catch (Exception ex)
            {
                context.Result = new GrantValidationResult(TokenRequestErrors.InvalidGrant, "用户名或密码不正确");
            }
           
            
        }

        public static Claim[] GetUserClaims(ApplicationUser user)
        {
            return new Claim[] {
                new Claim("user_id", user.Id.ToString()),
                new Claim("GroupId", user.GroupId),
                new Claim("uname", user.UserName)

                //roles
            };
        }
    }
}
