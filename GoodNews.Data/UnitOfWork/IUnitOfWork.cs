using System;
using System.Collections.Generic;
using System.Text;
using GoodNews.DAL.Entities;
using GoodNews.DAL.Repository;

namespace GoodNews.DAL.UnitOfWork
{
    public interface IUnitOfWork : IDisposable
    {
        IRepository<Article> News { get; }
        IRepository<Source> Sources { get; }
        IRepository<Category> Categories { get; }

        void Save();
    }
}
