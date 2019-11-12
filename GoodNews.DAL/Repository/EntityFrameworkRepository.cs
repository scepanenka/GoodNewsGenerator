using System;
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

        public virtual T GetById(object id)
        {
            return _table.Find(id);
        }

        public async Task<T> GetByIdAsync(object id)
        {
            return await _table.FindAsync(id);
        }


        public void Add(T obj)
        {
            _table.Add(obj);
        }

        public async Task AddAsync(T obj)
        {
            await _table.AddAsync(obj);
        }

        public void AddRange(IEnumerable<T> objects)
        {
            _table.AddRange(objects);
        }

        public async Task AddRangeAsync(IEnumerable<T> objects)
        {
            await _table.AddRangeAsync(objects);
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

        public IEnumerable<T> Find(Func<T, bool> predicate)
        {
            return _table.Where(predicate);
        }
        
    }
}
