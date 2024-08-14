using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using TLS.DataProvider.Entities;

namespace TLS.DataProvider
{
    public class ApplicationDbContext : IdentityDbContext<AppUser,AppRole,string>
    {
        public ApplicationDbContext(DbContextOptions options) : base(options)
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
            // Enable Lazy loading
            optionsBuilder.UseLazyLoadingProxies();
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            foreach (var entity in builder.Model.GetEntityTypes())
            {
                entity.SetTableName(entity.GetTableName().ToLower());
            }
            builder.Entity<AppUser>().ToTable("appusers");
            builder.Entity<AppRole>().ToTable("approles");
        }

        public DbSet<AppUser> appusers { get; set; }
        public DbSet<AppRole> approles { get; set; }
        public DbSet<Contact> contacts  { get; set; }
        public DbSet<News> news  { get; set; }
        public DbSet<Survey> surveys { get; set; }
        public DbSet<PlanExecutionInfo> planexecutioninfos { get; set; }


    }
}
