using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GoodNews.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace GoodNews.DAL.Repository
{
    public class EntityFrameworkRepository<T> : IRepository<T> where T : Entity
    {

        private readonly GoodNewsContext _context;
        private readonly DbSet<T> _table;


        public EntityFrameworkRepository(GoodNewsContext _context)
        {
            this._context = _context;
            _table = _context.Set<T>();
        }

        public IEnumerable<T> GetAll()
        {
            return _table.ToList();
        }

        public async Task<List<T>> GetAllAsync()
        {
            return await _table.ToListAsync();
        }

        public T GetById(object id)
        {
            return _table.Find(id);
        }

        public void Insert(T obj)
        {
            _table.Add(obj);
        }

        public void Update(T obj)
        {
            _table.Attach(obj);
            _context.Entry(obj).State = EntityState.Modified;
        }

        public void Delete(object id)
        {
            T existing = _table.Find(id);
            _table.Remove(existing);
        }

        public IQueryable<T> AsQueryable()
        {
            return _table.AsQueryable();
        }
    }
}
