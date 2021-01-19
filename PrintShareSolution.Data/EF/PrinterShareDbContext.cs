using eShopSolution.Data.Configurations;
using eShopSolution.Data.Extensions;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using PrintShareSolution.Data.Configurations;
using PrintShareSolution.Data.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace PrintShareSolution.Data.EF
{
    public class PrinterShareDbContext : IdentityDbContext<AppUser, AppRole, Guid>
    {
        public PrinterShareDbContext(DbContextOptions options) : base(options)
        {
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //Configure using Fluent API
            modelBuilder.ApplyConfiguration(new OrderPrintFileConfiguration());
            modelBuilder.ApplyConfiguration(new PrinterConfiguration());
            modelBuilder.ApplyConfiguration(new ListPrinterOfUserConfiguration());
            modelBuilder.ApplyConfiguration(new BlockListConfiguration());

            modelBuilder.ApplyConfiguration(new AppConfigConfiguration());
            modelBuilder.ApplyConfiguration(new AppUserConfiguration());
            modelBuilder.ApplyConfiguration(new AppRoleConfiguration());
           


            //Identity
            modelBuilder.Entity<IdentityUserClaim<Guid>>().ToTable("AppUserClaims");
            modelBuilder.Entity<IdentityUserRole<Guid>>().ToTable("AppUserRoles").HasKey(x => new { x.UserId, x.RoleId });
            modelBuilder.Entity<IdentityUserLogin<Guid>>().ToTable("AppUserLogins").HasKey(x => x.UserId);

            modelBuilder.Entity<IdentityRoleClaim<Guid>>().ToTable("AppRoleClaims");
            modelBuilder.Entity<IdentityUserToken<Guid>>().ToTable("AppUserTokens").HasKey(x => x.UserId);

            modelBuilder.Seed();
        }
        public DbSet<Printer> Printers { get; set; }
        public DbSet<AppConfig> AppConfigs { get; set; }
        public DbSet<OrderPrintFile>OrderPrintFiles { get; set; }
        public DbSet<ListPrinterOfUser>ListPrinterOfUsers { get; set; }
        public DbSet<BlockList>BlockLists { get; set; }
    }
}
