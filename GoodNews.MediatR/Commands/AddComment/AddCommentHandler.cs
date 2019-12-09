using System;
using System.Threading;
using System.Threading.Tasks;
using GoodNews.Data;
using MediatR;

namespace GoodNews.MediatR.Commands.AddComment
{
    public class AddCommentHandler : IRequestHandler<AddComment, Guid>
    {
        private readonly GoodNewsContext _context;
        public AddCommentHandler(GoodNewsContext context)
        {
            _context = context;
        }
        public async Task<Guid> Handle(AddComment request, CancellationToken cancellationToken)
        {
            await _context.Comments.AddAsync(request.Comment, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);
            return request.Comment.Id;
        }
    }
}
