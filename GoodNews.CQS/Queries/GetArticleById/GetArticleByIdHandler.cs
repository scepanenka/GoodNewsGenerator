using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using GoodNews.DAL;
using GoodNews.Data.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace GoodNews.CQS.Queries.GetArticleById
{
    class GetArticleByIdHandler : IRequestHandler<GetArticleByIdQuery, Article>
    {
        private readonly GoodNewsContext _context;
        public async Task<Article> Handle(GetArticleByIdQuery request, CancellationToken cancellationToken)
        {
            var result = await _context.News.FirstOrDefaultAsync(n => n.Id.Equals(request.Id),
                cancellationToken: cancellationToken);
            return result;
        }
    }
}
