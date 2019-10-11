using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using DD.Electricity.Cloud.AuthServer.Data;
using DD.Electricity.Cloud.AuthServer.Data.Identity;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace DD.Electricity.Cloud.AuthServer
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var host = CreateWebHostBuilder(args).Build();
            using (var scope = host.Services.CreateScope())
            {
                var services = scope.ServiceProvider;
                try
                {
                    var userManager = services.GetRequiredService<UserManager<ApplicationUser>>();
                    var roleManager = services.GetRequiredService<RoleManager<ApplicationRole>>();

                    //第一次添加的种子数据
                    //SeedData.EnsureSeedData(services, userManager, roleManager).Wait();

                    //徐总
                    SeedData.CreateForXuZong(userManager, roleManager, "18653207207").Wait();
                }
                catch (Exception ex)
                {
                    var logger = services.GetRequiredService<ILogger<Program>>();
                    logger.LogError(ex, "An error occurred while migrating and seeding the database.");
                }
            }
            host.Run();
        }

        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                //.UseKestrel(options =>
                //{
                //    options.Limits.MaxConcurrentConnections = 2000;
                //    options.Limits.MaxConcurrentUpgradedConnections = 2000;
                //})
                //.UseIISIntegration()
                //.UseIIS()
                .UseStartup<Startup>()
                .UseUrls("http://0.0.0.0:5000");
    }
}
