using GoodNews.Data;
using GoodNews.Data.Entities;

namespace GoodNews.DAL.Repository
{
    public class SourceRepository : EntityFrameworkRepository<Source>
    {
        public SourceRepository(GoodNewsContext _context) : base(_context)
        {
        }

    }
}
