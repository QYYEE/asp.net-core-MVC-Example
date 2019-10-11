using DD.Electricity.Cloud.AuthServer.Data.Identity;
using IdentityServer4.Extensions;
using IdentityServer4.Models;
using IdentityServer4.Services;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace DD.Electricity.Cloud.AuthServer.Extensions
{
    public class ProfileService : IProfileService
    {

        private readonly IUserClaimsPrincipalFactory<ApplicationUser> _claimsFactory;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IHostingEnvironment _hostingEnvironment;

        public ProfileService(UserManager<ApplicationUser> userManager, IUserClaimsPrincipalFactory<ApplicationUser> claimsFactory, IHostingEnvironment hostingEnvironment)
        {
            _userManager = userManager;
            _claimsFactory = claimsFactory;
            _hostingEnvironment = hostingEnvironment;
        }

        public async Task GetProfileDataAsync(ProfileDataRequestContext context)
        {
            if(_hostingEnvironment.IsDevelopment())
            {
                context.IssuedClaims.AddRange(context.Subject.Claims);
            }
            else
            {
                var sub = context.Subject.GetSubjectId();
                var user = await _userManager.FindByIdAsync(sub);
                var principal = await _claimsFactory.CreateAsync(user);//ClaimsPrincipal
                var claims = principal.Claims.ToList();//Claim
                claims = claims.Where(claim => context.RequestedClaimTypes.Contains(claim.Type)).ToList();
                claims.Add(new Claim("uname", user.UserName));
                claims.Add(new Claim("GroupId", user.GroupId));
                context.IssuedClaims = claims;
                //context.IssuedClaims.AddRange(context.Subject.Claims);
                //context.IssuedClaims.AddRange(new List<Claim> {
                //    new Claim("uname", user.UserName),
                //    new Claim("GroupId", user.GroupId)
                //});
            }
            
        }

        public async Task IsActiveAsync(IsActiveContext context)
        {
            if(!_hostingEnvironment.IsDevelopment())
            {
                var sub = context.Subject.GetSubjectId();
                var user = await _userManager.FindByIdAsync(sub);
                context.IsActive = user != null;
            }

            
        }
    }
}
