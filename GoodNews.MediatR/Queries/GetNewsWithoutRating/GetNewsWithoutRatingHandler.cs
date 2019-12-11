using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using GoodNews.Data;
using GoodNews.Data.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace GoodNews.MediatR.Queries.GetNewsWithoutRating
{
    public class GetNewsWithoutRatingHandler : IRequestHandler<GetNewsWithoutRating, IEnumerable<Article>>
    {
        private readonly GoodNewsContext _context;

        public GetNewsWithoutRatingHandler(GoodNewsContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Article>> Handle(GetNewsWithoutRating request, CancellationToken cancellationToken)
        {
            var news = await _context.News.Where(a => a.SentimentRating == null).ToListAsync(cancellationToken);
            return news;
        }
    }
}