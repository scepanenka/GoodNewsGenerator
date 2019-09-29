using System;
using System.Collections.Generic;
using System.Text;
using GoodNews.DAL.Entities;

namespace GoodNews.DAL.Repository
{
    public class CategoryRepository : EntityFrameworkRepository<Category>
    {
        public CategoryRepository(GoodNewsContext _context) : base(_context)
        {
        }
    }
}
