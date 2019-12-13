using System;
using System.Threading;
using System.Threading.Tasks;
using GoodNews.Data;
using MediatR;
using Serilog;

namespace GoodNews.MediatR.Commands.AddNews
{
    class AddNewsAsyncHandler : IRequestHandler<AddNews, bool>
    {
        private readonly GoodNewsContext _context;

        public AddNewsAsyncHandler(GoodNewsContext context)
        {
            _context = context;
        }
        public async Task<bool> Handle(AddNews request, CancellationToken cancellationToken)
        {
            if (request.News != null)
            {
                try
                {
                    await _context.News.AddRangeAsync(request.News, cancellationToken);
                    await _context.SaveChangesAsync(cancellationToken);
                    return true;
                }
                catch (Exception e)
                {
                    Log.Error($"GoodNews.MediatR.Commands.AddNews.AddRangeAsync -> Error adding news: {Environment.NewLine} {e.Message}");
                    return false; 
                }
            }

            return false;
        }
    }
}
