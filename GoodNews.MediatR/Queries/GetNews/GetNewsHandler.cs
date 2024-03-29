﻿using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using GoodNews.Data;
using GoodNews.Data.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace GoodNews.MediatR.Queries.GetNews
{
    public class GetNewsHandler : IRequestHandler<GetNews, IEnumerable<Article>>
    {
        private readonly GoodNewsContext _context;

        public GetNewsHandler(GoodNewsContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<Article>> Handle(GetNews request, CancellationToken cancellationToken)
        {
            var news = await _context.News.Include(a=>a.Source)
                .OrderByDescending(a=>a.DatePublication).Take(30).ToListAsync(cancellationToken);
            return news;
        }
    }
}
