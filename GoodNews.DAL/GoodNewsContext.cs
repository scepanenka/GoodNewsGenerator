using System;
using GoodNews.Data.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace GoodNews.DAL
{
    public class GoodNewsContext : IdentityDbContext<User>
    {
        public DbSet<Article> News { get; set; }
        public DbSet<Source> Sources { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Comment> Comments { get; set; }

        public GoodNewsContext(DbContextOptions<GoodNewsContext> options)
            : base(options)
        {
            
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Source>().HasData(
                new Source
                {
                    Id = Guid.NewGuid(),
                    Name = "Onliner",
                    Description = "Новости onliner.by",
                    Url = "https://people.onliner.by/feed",
                    QuerySelector = ".news-text"
                },
                new Source
                {
                    Id = Guid.NewGuid(),
                    Name = "S13",
                    Description = "Новости s13",
                    Url = "http://s13.ru/rss",
                    QuerySelector = ".js-mediator-article"
                },
                new Source
                {
                    Id = Guid.NewGuid(), Name = "Tut.by",
                    Description = "Новости tut.by",
                    Url = "https://news.tut.by/rss/all.rss",
                    QuerySelector = "#article_body"
                }
                );
        }
    }
}

