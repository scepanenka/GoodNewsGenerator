using System;
using System.Threading.Tasks;
using GoodNews.DAL.Repository;
using GoodNews.Data.Entities;

namespace Core
{
    public interface IUnitOfWork : IDisposable
    {
        IRepository<Article> News { get; }
        IRepository<Source> Sources { get; }
        IRepository<Category> Categories { get; }

        Category GetOrCreateCategory(string name);

        void Save();

        Task SaveAsync();
    }
}
