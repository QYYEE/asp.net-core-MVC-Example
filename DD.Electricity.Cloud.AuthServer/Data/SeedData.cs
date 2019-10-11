using DD.Electricity.Cloud.AuthServer.Data.Identity;
using IdentityServer4.EntityFramework.DbContexts;
using IdentityServer4.EntityFramework.Mappers;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DD.Electricity.Cloud.AuthServer.Data
{
    public class SeedData
    {
        /// <summary>
        /// 种子化各数据库
        /// </summary>
        /// <param name="serviceProvider"></param>

        public static async Task EnsureSeedData(IServiceProvider serviceProvider, UserManager<ApplicationUser> userManager, RoleManager<ApplicationRole> roleManager)
        {
            Console.WriteLine("seeding databas...");
            PerfomMigrations(serviceProvider);

            EnsureSeedData(serviceProvider.GetRequiredService<ConfigurationDbContext>());
            await EnsureUserSeedData(serviceProvider.GetRequiredService<ApplicationDbContext>(), userManager, roleManager);
            Console.WriteLine("done seeding database");
        }

        /// <summary>
        /// 确保先迁移数据库
        /// </summary>
        /// <param name="serviceProvider"></param>
        private static void PerfomMigrations(IServiceProvider serviceProvider)
        {
            serviceProvider.GetRequiredService<ApplicationDbContext>().Database.Migrate();
            serviceProvider.GetRequiredService<ConfigurationDbContext>().Database.Migrate();
            serviceProvider.GetRequiredService<PersistedGrantDbContext>().Database.Migrate();
        }

        /// <summary>
        /// 验证服务器种子数据
        /// </summary>
        /// <param name="context"></param>
        private static void EnsureSeedData(ConfigurationDbContext context)
        {
            if(!context.Clients.Any())
            {
                Console.WriteLine("clients beging populated");
                foreach(var client in Config.GetClients().ToList())
                {
                    context.Clients.Add(client.ToEntity());
                }
                context.SaveChanges();
            }
            else
            {
                Console.WriteLine("clients already populated");
            }

            if(!context.IdentityResources.Any())
            {
                Console.WriteLine("IdentityResources being populated");
                foreach(var resource in Config.GetIdentityResources())
                {
                    context.IdentityResources.Add(resource.ToEntity());
                }
                context.SaveChanges();
            }
            else
            {
                Console.WriteLine("IdentityResources already populated");
            }

            if(!context.ApiResources.Any())
            {
                Console.WriteLine("ApiResources being populated");
                foreach(var resource in Config.GetApiResources().ToList())
                {
                    context.ApiResources.Add(resource.ToEntity());
                }
                context.SaveChanges();
            }
            else
            {
                Console.WriteLine("ApiResources already populated");
            }
        }

        private static async Task EnsureUserSeedData(ApplicationDbContext applicationDbContext, UserManager<ApplicationUser> userManager, RoleManager<ApplicationRole> roleManager)
        {
            Console.WriteLine("users being populated");
            if (!applicationDbContext.Users.Any())
            {
                await CreateDefaultUserAndRoleForApplication(userManager, roleManager);
            }
            else
            {
                Console.WriteLine("users already populated");
            }
        }


        #region 用户相关

        /// <summary>
        /// 徐总创建账号密码
        /// </summary>
        /// <param name="userManager"></param>
        /// <param name="roleManager"></param>
        /// <param name="userName"></param>
        /// <returns></returns>
        public static async Task CreateForXuZong(UserManager<ApplicationUser> userManager, RoleManager<ApplicationRole> roleManager, string userName)
        {
            //如果不存在才创建
            var currentXu = await userManager.FindByNameAsync(userName);
            if(currentXu == null)
            {
                var xuUser = await CreateDefaultUser(userManager, userName, ProjectDic.project_id_nuode, ProjectDic.group_id_nuode);
                await SetPasswordForDefaultUser(userManager, userName, xuUser, "taihe001");
                await AddDefaultRoleToDefaultUser(userManager, userName, "管理员", xuUser);
            }
        }


        /// <summary>
        /// 创建角色、用户、密码
        /// </summary>
        /// <param name="userManager"></param>
        /// <param name="roleManager"></param>
        /// <returns></returns>
        private static async Task CreateDefaultUserAndRoleForApplication(UserManager<ApplicationUser> userManager, RoleManager<ApplicationRole> roleManager)
        {
            const string adminRole = "SuperAdmin";
            const string gongZuoRenYuanRole = "DingDing";
            const string sheBeiJingLiRole = "DeviceAdmin";
            const string dianGongRole = "DianGong";
            const string managerRole = "管理员";

            const string mobile = "13800000000";//管理员用户名
            const string gzryMobile = "dingding"; //工作人员用户名
            const string sbjlMobile = "shebei";//设备经理用户名
            const string dgMobole = "diangong"; //电工用户名
            const string managerMobile = "dingding001";

            //创建角色
            await CreateDefaultAdministratorRole(roleManager, adminRole, ProjectDic.project_id_nuode, ProjectDic.group_id_nuode);
            await CreateDefaultAdministratorRole(roleManager, gongZuoRenYuanRole, ProjectDic.project_id_nuode, ProjectDic.group_id_nuode);
            await CreateDefaultAdministratorRole(roleManager, sheBeiJingLiRole, ProjectDic.project_id_nuode, ProjectDic.group_id_nuode);
            await CreateDefaultAdministratorRole(roleManager, dianGongRole, ProjectDic.project_id_nuode, ProjectDic.group_id_nuode);
            await CreateDefaultAdministratorRole(roleManager, managerRole, ProjectDic.project_id_nuode, ProjectDic.group_id_nuode);

            //创建用户，设置密码，分配角色
            var adminUser = await CreateDefaultUser(userManager, mobile, ProjectDic.project_id_nuode,ProjectDic.group_id_nuode);
            await SetPasswordForDefaultUser(userManager, mobile, adminUser, ProjectDic.project_nuode_admin_pass);
            await AddDefaultRoleToDefaultUser(userManager, mobile, adminRole, adminUser);

            var gzryUser = await CreateDefaultUser(userManager, gzryMobile, ProjectDic.project_id_nuode, ProjectDic.group_id_nuode);
            await SetPasswordForDefaultUser(userManager, gzryMobile, gzryUser, ProjectDic.project_nuode_gzry_pass);
            await AddDefaultRoleToDefaultUser(userManager, gzryMobile, gongZuoRenYuanRole, gzryUser);

            var sbjlUser = await CreateDefaultUser(userManager, sbjlMobile, ProjectDic.project_id_nuode, ProjectDic.group_id_nuode);
            await SetPasswordForDefaultUser(userManager, sbjlMobile, sbjlUser, ProjectDic.project_nuode_sb_pass);
            await AddDefaultRoleToDefaultUser(userManager, sbjlMobile, sheBeiJingLiRole, sbjlUser);

            var dgUser = await CreateDefaultUser(userManager, dgMobole, ProjectDic.project_id_nuode, ProjectDic.group_id_nuode);
            await SetPasswordForDefaultUser(userManager, dgMobole, dgUser, ProjectDic.project_nuode_dg_pass);
            await AddDefaultRoleToDefaultUser(userManager, dgMobole, dianGongRole, dgUser);

            //集团测试账户
            var managerUser = await CreateDefaultUser(userManager, managerMobile, ProjectDic.project_id_nuode, ProjectDic.group_id_nuode);
            await SetPasswordForDefaultUser(userManager, managerMobile, managerUser, "dingding001");
            await AddDefaultRoleToDefaultUser(userManager, managerMobile, managerRole, managerUser);
        }

        /// <summary>
        /// 创建角色
        /// </summary>
        /// <param name="roleManager"></param>
        /// <param name="roleName"></param>
        /// <returns></returns>
        private static async Task CreateDefaultAdministratorRole(RoleManager<ApplicationRole> roleManager, string roleName, string projectIds, string groupId)
        {
            Console.WriteLine($"create the role `{roleName}` for application");
            var result = await roleManager.CreateAsync(new ApplicationRole { Name = roleName, ProjectIds = projectIds, GroupId=groupId});
            if(result.Succeeded)
            {
                Console.WriteLine($"created the role `{roleName}` successfully");
            }
            else
            {
                var exception = new ApplicationException($"Default role `{roleName}` cannot be created");
                Console.WriteLine(GetIdentityErrorsInCommaSeperatedList(result));
                throw exception;
            }
        }

        /// <summary>
        /// 创建用户
        /// </summary>
        /// <param name="userManager"></param>
        /// <param name="mobile"></param>
        /// <returns></returns>
        private static async Task<ApplicationUser> CreateDefaultUser(UserManager<ApplicationUser> userManager, string mobile, string projectId, string groupId)
        {
            Console.WriteLine($"create default user with mobile `{mobile}` for application");
            var user = new ApplicationUser { UserName = mobile, ProjectIds=projectId, GroupId=groupId };
            var result = await userManager.CreateAsync(user);
            if(result.Succeeded)
            {
                Console.WriteLine($"created default user `{mobile}` successfully");
            }
            else
            {
                var excetpion = new ApplicationException($"Deault user `{mobile}` cannot be created");
                Console.WriteLine(GetIdentityErrorsInCommaSeperatedList(result));
                throw excetpion;
            }
            var createdUser = await userManager.FindByNameAsync(mobile);
            return createdUser;
        }

        /// <summary>
        /// 为用户创建密码
        /// </summary>
        /// <param name="userManager"></param>
        /// <param name="mobile"></param>
        /// <param name="user"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        private static async Task SetPasswordForDefaultUser(UserManager<ApplicationUser> userManager, string mobile, ApplicationUser user, string password)
        {
            Console.WriteLine($"set password for default user `{mobile}`");
            var result = await userManager.AddPasswordAsync(user, password);
            if(result.Succeeded)
            {
                Console.WriteLine($"set password `{password}` for default user `{mobile}` successfully");
            }
            else
            {
                var exception = new ApplicationException($"password for the user `{mobile}` cannot be set");
                Console.WriteLine(GetIdentityErrorsInCommaSeperatedList(result));
                throw exception;
            }
        }

        /// <summary>
        /// 把角色加到用户上
        /// </summary>
        /// <param name="userManager"></param>
        /// <param name="mobile"></param>
        /// <param name="administratorRole"></param>
        /// <param name="user"></param>
        /// <returns></returns>
        private static async Task AddDefaultRoleToDefaultUser(UserManager<ApplicationUser> userManager, string mobile, string administratorRole, ApplicationUser user)
        {
            Console.WriteLine($"add default `{mobile}` to role `{administratorRole}`");
            var result = await userManager.AddToRoleAsync(user, administratorRole);
            if(result.Succeeded)
            {
                Console.WriteLine($"add role `{administratorRole}` to default user `{mobile}` successfully");
            }
            else
            {
                var exception = new ApplicationException($"the role `{administratorRole}` cannot be set for the user `{mobile}`");
                Console.WriteLine(GetIdentityErrorsInCommaSeperatedList(result));
                throw exception;
            }
        }

        private static string GetIdentityErrorsInCommaSeperatedList(IdentityResult result)
        {
            string errors = string.Empty;
            foreach(var identityError in result.Errors)
            {
                errors += identityError.Description;
                errors += ",";
            }
            return errors;
        }
        #endregion
    }
}
