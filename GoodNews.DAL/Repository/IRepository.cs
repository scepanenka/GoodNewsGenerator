using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GoodNews.DAL.Repository
{
    public interface IRepository<T> where T : class
    {
        IEnumerable<T> GetAll();
        Task<List<T>> GetAllAsync();
        T GetById(object id);
        Task<T> GetByIdAsync(object id);


        void Add(T obj);

        Task AddAsync(T obj);

        void AddRange(IEnumerable<T> objects);
        Task AddRangeAsync(IEnumerable<T> objects);
        void Update(T obj);
        void Delete(object id);

        IQueryable<T> AsQueryable();

        IEnumerable<T> Find(Func<T, bool> predicate);
    }
}
