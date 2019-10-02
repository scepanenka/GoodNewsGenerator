using System;
using System.Collections.Generic;
using System.Text;
using GoodNews.DAL.Entities;
using Microsoft.EntityFrameworkCore;

namespace GoodNews.DAL
{
    public class GoodNewsContext : DbContext
    {
        public DbSet<Article> News { get; set; }
        public DbSet<Source> Sources { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<UserRole> UserRoles { get; set; }

        public GoodNewsContext(DbContextOptions<GoodNewsContext> options)
            : base(options)
        {
            Database.EnsureCreated();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<UserRole>().HasKey(ur => new { ur.UserId, ur.RoleId });
        }
    }
}
