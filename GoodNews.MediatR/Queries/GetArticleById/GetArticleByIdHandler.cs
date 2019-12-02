using System.Threading;
using System.Threading.Tasks;
using GoodNews.Data;
using GoodNews.Data.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace GoodNews.MediatR.Queries.GetArticleById
{
    class GetArticleByIdHandler : IRequestHandler<GetArticleById, Article>
    {
        private readonly GoodNewsContext _context;

        public GetArticleByIdHandler(GoodNewsContext context)
        {
            _context = context;
        }
        public async Task<Article> Handle(GetArticleById request, CancellationToken cancellationToken)
        {
            var article = await _context.News.FirstOrDefaultAsync(n => n.Id.Equals(request.Id),
                cancellationToken: cancellationToken);
            return article;
        }
    }
}
