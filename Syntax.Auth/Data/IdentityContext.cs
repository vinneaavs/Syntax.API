using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Syntax.Auth.Data
{
    public class IdentityContext : IdentityDbContext<ApplicationUser, IdentityRole, string>
    {
        public DbSet<LoginLog> LoginLogs { get; set; }

        public IdentityContext(DbContextOptions<IdentityContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Configurações padrão do Identity
            modelBuilder.Entity<IdentityRole>().ToTable("AspNetRoles").HasKey(x => x.Id);
            modelBuilder.Entity<IdentityUserRole<string>>().ToTable("AspNetUserRoles").HasKey(x => new { x.UserId, x.RoleId });
            modelBuilder.Entity<IdentityUserLogin<string>>().ToTable("AspNetUserLogins").HasKey(x => x.UserId);
            modelBuilder.Entity<IdentityUserToken<string>>().ToTable("AspNetUserTokens").HasKey(x => x.UserId);
            modelBuilder.Entity<IdentityUserClaim<string>>().ToTable("AspNetUserClaims").HasKey(x => x.Id);
            modelBuilder.Entity<IdentityRoleClaim<string>>().ToTable("AspNetRoleClaims").HasKey(x => x.Id);



            // Configurações da tabela LoginLog
            modelBuilder.Entity<LoginLog>().ToTable("LoginLog");
            modelBuilder.Entity<LoginLog>().HasKey(x => x.Id);
            modelBuilder.Entity<LoginLog>().Property(x => x.Id).ValueGeneratedOnAdd();
            modelBuilder.Entity<LoginLog>().Property(x => x.IdUser).IsRequired();
            modelBuilder.Entity<LoginLog>().Property(x => x.LoginTime).IsRequired();
        }
    }


}
