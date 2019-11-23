using System;
using System.Threading.Tasks;
using GoodNews.Data.Entities;

namespace GoodNews.Core
{
    public interface IUnitOfWork : IDisposable
    {
        IRepository<Article> News { get; }
        IRepository<Source> Sources { get; }
        IRepository<Category> Categories { get; }
        IRepository<Comment> Comments { get; }

        Category GetOrCreateCategory(string name);


        void Save();

        Task SaveAsync();
    }
}
