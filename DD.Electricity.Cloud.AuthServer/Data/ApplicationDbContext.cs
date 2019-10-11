using DD.Electricity.Cloud.AuthServer.Data.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DD.Electricity.Cloud.AuthServer.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser, ApplicationRole, string>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder builder)
        {        
            base.OnModelCreating(builder);


            #region 通过IEntityTypeConfiguration
            //builder.ApplyConfigurationsFromAssembly(typeof(ApplicationDbContext).Assembly); 
            #endregion

            #region 导入MySQL数据库需要考虑的方面
            builder.Entity<ApplicationUser>(entity =>
            {
                entity.Property(t => t.Id).HasMaxLength(63);
                entity.Property(t => t.UserName).HasMaxLength(63);
                entity.Property(t => t.NormalizedUserName).HasMaxLength(63);
                entity.Property(t => t.NormalizedEmail).HasMaxLength(63);
            });

            builder.Entity<ApplicationUser>().Ignore(t => t.LockoutEnd);

            builder.Entity<ApplicationRole>(entity =>
            {
                entity.Property(t => t.Id).HasMaxLength(63);
                entity.Property(t => t.Name).HasMaxLength(63);
                entity.Property(t => t.NormalizedName).HasMaxLength(63);
            });

            builder.Entity<IdentityUserLogin<string>>(entity =>
            {
                entity.Property(t => t.LoginProvider).HasMaxLength(63);
                entity.Property(m => m.ProviderKey).HasMaxLength(63);
            });

            builder.Entity<IdentityUserRole<string>>(entity =>
            {
                entity.Property(t => t.UserId).HasMaxLength(63);
                entity.Property(t => t.RoleId).HasMaxLength(63);
            });

            builder.Entity<IdentityUserToken<string>>(entity =>
            {
                entity.Property(t => t.UserId).HasMaxLength(63);
                entity.Property(t => t.LoginProvider).HasMaxLength(63);
                entity.Property(t => t.Name).HasMaxLength(63);
            }); 
            #endregion
        }
    }
}
