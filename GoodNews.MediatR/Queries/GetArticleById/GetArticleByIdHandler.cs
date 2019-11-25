using System.Threading;
using System.Threading.Tasks;
using GoodNews.Data;
using GoodNews.Data.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace GoodNews.MediatR.Queries.GetArticleById
{
    class GetCommentsByArticleIdHandler : IRequestHandler<GetArticleByIdQuery, Article>
    {
        private readonly GoodNewsContext _context;

        public GetCommentsByArticleIdHandler(GoodNewsContext context)
        {
            _context = context;
        }
        public async Task<Article> Handle(GetArticleByIdQuery request, CancellationToken cancellationToken)
        {
            var result = await _context.News.FirstOrDefaultAsync(n => n.Id.Equals(request.Id),
                cancellationToken: cancellationToken);
            return result;
        }
    }
}
