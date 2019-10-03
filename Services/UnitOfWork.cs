using System;
using Core;
using GoodNews.DAL;
using GoodNews.DAL.Repository;
using GoodNews.Data.Entities;

namespace Services
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly GoodNewsContext _context;
        
        private readonly IRepository<Article> _newsRepository;
        private readonly IRepository<Category> _categoriesRepository;
        private readonly IRepository<Source> _sourcesRepository;
        private bool disposed = false;

        public UnitOfWork(GoodNewsContext context,
            IRepository<Article> news,
            IRepository<Source> sources,
            IRepository<Category> categories)
        {
            _context = context;
            _newsRepository = news;
            _categoriesRepository = categories;
            _sourcesRepository = sources;
        }



        public IRepository<Article> News => _newsRepository;

        public IRepository<Source> Sources => _sourcesRepository;

        public IRepository<Category> Categories => _categoriesRepository;

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
