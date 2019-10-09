﻿using System;
using System.Collections.Generic;
using System.Text;
using GoodNews.Data.Entities;
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
        public DbSet<Comment> Comments { get; set; }

        public GoodNewsContext(DbContextOptions<GoodNewsContext> options)
            : base(options)
        {
            Database.EnsureCreated();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<UserRole>().HasKey(ur => new { ur.UserId, ur.RoleId });

            modelBuilder.Entity<UserRole>()
                .HasOne<User>(sc => sc.User)
                .WithMany(s => s.UserRoles)
                .HasForeignKey(sc => sc.UserId);


            modelBuilder.Entity<UserRole>()
                .HasOne<Role>(sc => sc.Role)
                .WithMany(s => s.UserRoles)
                .HasForeignKey(sc => sc.RoleId);
        }
    }
}