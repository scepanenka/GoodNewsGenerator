using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using GoodNews.Data;
using GoodNews.Data.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace GoodNews.MediatR.Queries.GetCommentsByArticleId
{
    class GetCommentsByArticleIdHandler : IRequestHandler<GetCommentsByArticleId, IEnumerable<Comment>>
    {
        private readonly GoodNewsContext _context;

        public GetCommentsByArticleIdHandler(GoodNewsContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<Comment>> Handle(GetCommentsByArticleId request, CancellationToken cancellationToken)
        {
            var  result = await _context.Comments.Where(c => c.ArticleId.Equals(request.ArticleId))
                .Include(c => c.User).OrderByDescending(c => c.Date).ToListAsync(cancellationToken);
            return result;
        }
    }
}
