using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Threading.Tasks;
using DD.Electricity.Cloud.AuthServer.Data;
using DD.Electricity.Cloud.AuthServer.Data.Identity;
using DD.Electricity.Cloud.AuthServer.Extensions;
using DD.Electricity.Cloud.HdPersistence;
using IdentityServer4.Services;
using IdentityServer4.Validation;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Serilog;

namespace DD.Electricity.Cloud.AuthServer
{
    public class Startup
    {
        //development branch test
        //development branch second
        //development branch from nomal user
        public IConfiguration Configuration { get; }
        public IHostingEnvironment Environment { get; }


        public Startup(IConfiguration configuration, IHostingEnvironment environment)
        {
            Configuration = configuration;
            Environment = environment;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            var migrationAssembly = typeof(Startup).GetTypeInfo().Assembly.GetName().Name;
            #region 上下文

            services.AddDbContext<ApplicationDbContext>(options => options.UseMySql(Configuration.GetConnectionString("Default")));
            services.AddMvc();
            services.AddDbContext<HdDbContext>(options => options.UseMySql(Configuration.GetConnectionString(GlobalSettings.DbContext_DingDing)));//总部上下文 
            #endregion

            #region 迁移数据库种子化数据库用
            //        services.AddIdentity<ApplicationUser, ApplicationRole>(options =>
            //{
            //    options.Password.RequireDigit = false;
            //    options.Password.RequireLowercase = false;
            //    options.Password.RequireNonAlphanumeric = false;
            //    options.Password.RequireUppercase = false;
            //})
            //        .AddEntityFrameworkStores<ApplicationDbContext>()
            //        .AddDefaultTokenProviders();


            //        var builder = services.AddIdentityServer(options =>
            //        {
            //            options.Events.RaiseFailureEvents = true;
            //            options.Events.RaiseInformationEvents = true;
            //            options.Events.RaiseFailureEvents = true;
            //            options.Events.RaiseSuccessEvents = true;
            //        })
            //            .AddDeveloperSigningCredential()
            //            .AddTestUsers(Config.GetUsers().ToList())
            //            .AddInMemoryApiResources(Config.GetApiResources().ToList())
            //            .AddInMemoryClients(Config.GetClients().ToList())
            //            .AddAspNetIdentity<ApplicationUser>()
            //            .AddConfigurationStore(options =>
            //            {
            //                options.ConfigureDbContext = b =>
            //                    b.UseMySql(Configuration.GetConnectionString("Default"), sql => sql.MigrationsAssembly(migrationAssembly));
            //            })
            //            .AddOperationalStore(options =>
            //            {
            //                options.ConfigureDbContext = b =>
            //                    b.UseMySql(Configuration.GetConnectionString("Default"), sql => sql.MigrationsAssembly(migrationAssembly));
            //                options.EnableTokenCleanup = true;
            //                //options.TokenCleanupInterval = 15; //token刷新频率，秒为单位
            //            });

            //        services.AddAuthentication();
            #endregion

            #region 验证服务器

            if (Environment.IsDevelopment()) //开发环境服务器配置
            {
                #region 测试
                services.AddIdentity<ApplicationUser, ApplicationRole>(options =>
                {
                    options.Password.RequireDigit = false;
                    options.Password.RequireLowercase = false;
                    options.Password.RequireNonAlphanumeric = false;
                    options.Password.RequireUppercase = false;
                })
                    .AddEntityFrameworkStores<ApplicationDbContext>()
                    .AddDefaultTokenProviders();

                var builder = services.AddIdentityServer(options =>
                {
                    options.Events.RaiseErrorEvents = true;
                    options.Events.RaiseInformationEvents = true;
                    options.Events.RaiseFailureEvents = true;
                    options.Events.RaiseSuccessEvents = true;
                })
                    .AddTestUsers(Config.GetUsers().ToList());


                builder.AddInMemoryIdentityResources(Config.GetIdentityResources());
                builder.AddInMemoryApiResources(Config.GetApiResources());
                builder.AddInMemoryClients(Config.GetClients());
                builder.AddProfileService<ProfileService>();

                if (Environment.IsDevelopment())
                {
                    builder.AddDeveloperSigningCredential();
                }
                //else
                //{
                //    throw new Exception("need configure key material");
                //}

                

                #endregion
            }
            else //生产环境认证服务器配置
            {
                #region 实际

                //Identity配置
                services.AddIdentity<ApplicationUser, ApplicationRole>(options =>
                {
                    options.Password.RequireDigit = false;
                    options.Password.RequireLowercase = false;
                    options.Password.RequireNonAlphanumeric = false;
                    options.Password.RequireUppercase = false;
                })
                    .AddEntityFrameworkStores<ApplicationDbContext>()
                    .AddDefaultTokenProviders();

                //Identity认证授权
                services.ConfigureApplicationCookie(options => {
                    options.LoginPath = "/Security/Login";
                    options.LogoutPath = "/Security/Logout";
                    options.AccessDeniedPath = "/Security/AccessDenied";
                    options.Cookie = new CookieBuilder
                    {
                        Name = "DD.Electricity.Cloud.AuthServer.Cookie",
                        Path = "/",
                        SameSite = SameSiteMode.Lax,
                        SecurePolicy = CookieSecurePolicy.SameAsRequest
                    };
                });


                //验证服务器的配置
                var builder = services.AddIdentityServer(options =>
                {
                    options.Events.RaiseFailureEvents = true;
                    options.Events.RaiseInformationEvents = true;
                    options.Events.RaiseFailureEvents = true;
                    options.Events.RaiseSuccessEvents = true;
                })
                    .AddDeveloperSigningCredential()
                    //.AddTestUsers(Config.GetUsers().ToList())
                    //.AddInMemoryApiResources(Config.GetApiResources().ToList())
                    //.AddInMemoryClients(Config.GetClients().ToList());
                    .AddAspNetIdentity<ApplicationUser>()
                    .AddConfigurationStore(options =>
                    {
                        options.ConfigureDbContext = b =>
                            b.UseMySql(Configuration.GetConnectionString("Default"), sql => sql.MigrationsAssembly(migrationAssembly));
                    })
                    .AddOperationalStore(options =>
                    {
                        options.ConfigureDbContext = b =>
                            b.UseMySql(Configuration.GetConnectionString("Default"), sql => sql.MigrationsAssembly(migrationAssembly));
                        options.EnableTokenCleanup = true;
                        //options.TokenCleanupInterval = 15; //token刷新频率，秒为单位
                    })
                   .AddProfileService<ProfileService>();

                //services.AddTransient<IResourceOwnerPasswordValidator, CustomResourceOwnerPasswordValidator>();
                //services.AddTransient<IProfileService, ProfileService>();

                #endregion
            }

            //services.AddScoped<IUserClaimsPrincipalFactory<ApplicationUser>, AppClaimsPrincipalFactory>();

            services.AddAuthentication();

            #endregion



            services.AddCors(options => options.AddPolicy("AllowAll", p => p.AllowAnyOrigin()
               .AllowAnyMethod()
               .AllowAnyHeader()));

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseExceptionHandler(builder =>
            {
                builder.Run(async context =>
                {
                    context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                    context.Response.Headers.Add("Access-Control-Allow-Origin", "*");

                    var error = context.Features.Get<IExceptionHandlerFeature>();
                    if (error != null)
                    {
                        context.Response.AddApplicationError(error.Error.Message);
                        await context.Response.WriteAsync(error.Error.Message).ConfigureAwait(false);
                    }
                });
            });

            var serilog = new LoggerConfiguration()
                .MinimumLevel.Verbose()
                .Enrich.FromLogContext()
                .WriteTo.File(@"authserver_log.txt");

            loggerFactory.WithFilter(new FilterLoggerSettings
                {
                    { "IdentityServer4", LogLevel.Debug },
                    { "Microsoft", LogLevel.Warning },
                    { "System", LogLevel.Warning },
                }).AddSerilog(serilog.CreateLogger());

            app.UseStaticFiles();
            app.UseCors("AllowAll");
            app.UseHttpsRedirection();
            app.UseAuthentication();//本站授权验证
            app.UseIdentityServer();//认证服务器

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
