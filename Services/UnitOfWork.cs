using System;
using System.Linq;
using System.Threading.Tasks;
using Core;
using GoodNews.Core;
using GoodNews.DAL;
using GoodNews.Data;
using GoodNews.Data.Entities;

namespace Services
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly GoodNewsContext _context;
        
        private readonly IRepository<Article> _newsRepository;
        private readonly IRepository<Category> _categoriesRepository;
        private readonly IRepository<Source> _sourcesRepository;
        private readonly IRepository<Comment> _commentsRepository;
        private bool disposed = false;

        public UnitOfWork(GoodNewsContext context,
            IRepository<Article> news,
            IRepository<Source> sources,
            IRepository<Comment> comments,
            IRepository<Category> categories)
        {
            _context = context;
            _newsRepository = news;
            _categoriesRepository = categories;
            _sourcesRepository = sources;
            _commentsRepository = comments;
        }



        public IRepository<Article> News => _newsRepository;

        public IRepository<Source> Sources => _sourcesRepository;

        public IRepository<Category> Categories => _categoriesRepository;
        public IRepository<Comment> Comments => _commentsRepository;

        public Category GetOrCreateCategory(string name)
        {
            Category category = _categoriesRepository.GetAll().FirstOrDefault(x => x.Name == name);
            if (category == null)
            {
                category = new Category
                {
                    Name = name
                };
                _categoriesRepository.Add(category);
                Save();
            }
            return category;
        }

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

        public async Task SaveAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
