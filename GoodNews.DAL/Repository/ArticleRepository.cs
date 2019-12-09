using GoodNews.Data;
using GoodNews.Data.Entities;

namespace GoodNews.DAL.Repository
{
    public class ArticleRepository : EntityFrameworkRepository<Article>
    {
        public ArticleRepository(GoodNewsContext _context) : base(_context)
        {
        }
    }
}
