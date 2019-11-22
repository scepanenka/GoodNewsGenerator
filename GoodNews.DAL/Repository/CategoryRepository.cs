using GoodNews.Data;
using GoodNews.Data.Entities;

namespace GoodNews.DAL.Repository
{
    public class CategoryRepository : EntityFrameworkRepository<Category>
    {
        public CategoryRepository(GoodNewsContext _context) : base(_context)
        {
        }
    }
}
