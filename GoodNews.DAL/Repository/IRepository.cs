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
        void Insert(T obj);
        void Update(T obj);
        void Delete(object id);

        IQueryable<T> AsQueryable();
    }
}
