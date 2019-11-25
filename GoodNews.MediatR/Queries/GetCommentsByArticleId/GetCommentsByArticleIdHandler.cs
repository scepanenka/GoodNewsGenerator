using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using GoodNews.Data;
using GoodNews.Data.Entities;
using GoodNews.MediatR.Queries.GetArticleById;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace GoodNews.MediatR.Queries.GetCommentsByArticleId
{
    class GetCommentsByArticleIdHandler : IRequestHandler<GetCommentsByArticleIdQuery, IEnumerable<Comment>>
    {
        private readonly GoodNewsContext _context;

        public GetCommentsByArticleIdHandler(GoodNewsContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<Comment>> Handle(GetCommentsByArticleIdQuery request, CancellationToken cancellationToken)
        {
            var  result = await _context.Comments.Include(c => c.User)
                    .Where(c => c.ArticleId.Equals(request.ArticleId)).OrderByDescending(c => c.Date).ToListAsync(cancellationToken);
            return result;
        }
    }
}
