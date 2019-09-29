using System;
using System.Collections.Generic;
using System.Text;
using GoodNews.DAL.Entities;

namespace GoodNews.DAL.Repository
{
    public class SourceRepository : EntityFrameworkRepository<Source>
    {
        public SourceRepository(GoodNewsContext _context) : base(_context)
        {
        }
    }
}
