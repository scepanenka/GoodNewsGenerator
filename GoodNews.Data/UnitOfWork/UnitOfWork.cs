using System;
using System.Collections.Generic;
using System.Text;
using GoodNews.DAL.Entities;
using GoodNews.DAL.Repository;

namespace GoodNews.DAL.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly GoodNewsContext _context;
        
        private readonly IRepository<Article> _articlesRepository;
        private readonly IRepository<Category> _categoriesRepository;
        private readonly IRepository<Source> _sourcesRepository;
        private bool disposed = false;

        public UnitOfWork(GoodNewsContext context,
            IRepository<Article> news,
            IRepository<Source> sources,
            IRepository<Category> categories)
        {
            _context = context;
            _articlesRepository = news;
            _categoriesRepository = categories;
            _sourcesRepository = sources;
        }


        public IRepository<Article> Orders { get; }
        public IRepository<Source> OrderItems { get; }
        public IRepository<Category> Products { get; }

        public virtual void Dispose(bool disposing)
        {
            if (!disposed)
            {
                if (disposing)
                {
                    _context.Dispose();
                }
                disposed = true;
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        public void Save()
        {
            _context.SaveChanges();

        }
    }
}
