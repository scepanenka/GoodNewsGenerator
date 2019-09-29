using System;
using System.Collections.Generic;
using System.Text;
using GoodNews.DAL.Entities;

namespace GoodNews.DAL.Repository
{
    public class ArticleRepository : EntityFrameworkRepository<Article>
    {
        public ArticleRepository(GoodNewsContext _context) : base(_context)
        {
        }
    }
}
