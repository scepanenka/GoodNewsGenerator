using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using GoodNews.Data;
using GoodNews.Data.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace GoodNews.MediatR.Queries.GetNewsByPage
{
    public class GetNewsByPageHandler : IRequestHandler<GetNewsByPage, IEnumerable<Article>>
    {
        private readonly GoodNewsContext _context;

        public GetNewsByPageHandler(GoodNewsContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Article>> Handle(GetNewsByPage request, CancellationToken cancellationToken)
        {
            int newsPerPage = 15;
            var news = await _context.News.Where(a=>a.SentimentRating > 0).Include(a => a.Source)
                .OrderByDescending(a => a.DatePublication).Skip((request.Page - 1) * newsPerPage).Take(newsPerPage).ToListAsync(cancellationToken);
            return news;
        }
    }
}
