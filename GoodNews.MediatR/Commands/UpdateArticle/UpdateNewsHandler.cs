using System;
using System.Threading;
using System.Threading.Tasks;
using GoodNews.Data;
using MediatR;
using Serilog;

namespace GoodNews.MediatR.Commands.UpdateArticle
{
    public class UpdateNewsHandler : IRequestHandler<UpdateNews, bool>
    {
        private readonly GoodNewsContext _context;

        public UpdateNewsHandler(GoodNewsContext context)
        {
            _context = context;
        }
        public async Task<bool> Handle(UpdateNews request, CancellationToken cancellationToken)
        {
            if (request.News != null)
            {
                try
                {
                    _context.News.UpdateRange(request.News);
                    await _context.SaveChangesAsync(cancellationToken);
                    return true;
                }
                catch (Exception e)
                {
                    Log.Error($"Error while update news: {Environment.NewLine} {e.Message}");
                    return false;
                    throw;
                }
            }

            return false;
        }
    }
}
