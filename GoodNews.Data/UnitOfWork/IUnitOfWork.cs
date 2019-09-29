using System;
using System.Collections.Generic;
using System.Text;
using GoodNews.DAL.Entities;
using GoodNews.DAL.Repository;

namespace GoodNews.DAL.UnitOfWork
{
    public interface IUnitOfWork : IDisposable
    {
        IRepository<Article> Orders { get; }
        IRepository<Source> OrderItems { get; }
        IRepository<Category> Products { get; }

        void Save();
    }
}
