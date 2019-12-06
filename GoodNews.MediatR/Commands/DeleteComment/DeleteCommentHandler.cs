using System.Threading;
using System.Threading.Tasks;
using GoodNews.Data;
using MediatR;

namespace GoodNews.MediatR.Commands.DeleteComment
{
    public class DeleteCommentHandler : IRequestHandler<DeleteComment, bool>
    {
        private readonly GoodNewsContext _context;

        public DeleteCommentHandler(GoodNewsContext context)
        {
            _context = context;
        }

        public async Task<bool> Handle(DeleteComment request, CancellationToken cancellationToken)
        {
            var comment = _context.Comments.Find(request.Id);
            _context.Comments.Remove(comment);
            await _context.SaveChangesAsync(cancellationToken);

            return true;
        }
    }
}
