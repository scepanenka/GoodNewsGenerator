using System.Threading;
using System.Threading.Tasks;
using GoodNews.Data;
using GoodNews.Data.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace GoodNews.MediatR.Queries.GetCommentById
{
    class GetCommentByIdHandler : IRequestHandler<GetCommentById, Comment>
    {
        private readonly GoodNewsContext _context;

        public GetCommentByIdHandler(GoodNewsContext context)
        {
            _context = context;
        }
        public async Task<Comment> Handle(GetCommentById request, CancellationToken cancellationToken)
        {
            var comment = await _context.Comments.FirstOrDefaultAsync(n => n.Id.Equals(request.Id),
                cancellationToken: cancellationToken);
            return comment;
        }
    }
}
