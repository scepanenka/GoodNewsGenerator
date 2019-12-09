using System;
using System.Threading;
using System.Threading.Tasks;
using GoodNews.Data;
using MediatR;
using Serilog;

namespace GoodNews.MediatR.Commands.AddArticle
{
    class AddNewsAsyncHandler : IRequestHandler<AddArticle, bool>
    {
        private readonly GoodNewsContext _context;

        public AddNewsAsyncHandler(GoodNewsContext context)
        {
            _context = context;
        }
        public async Task<bool> Handle(AddArticle request, CancellationToken cancellationToken)
        {
            if (request.Article != null)
            {
                try
                {
                    _context.News.Add(request.Article);
                    await _context.SaveChangesAsync(cancellationToken);
                    return true;
                }
                catch (Exception e)
                {
                    Log.Error($"GoodNews.MediatR.Commands.AddNews.AddRangeAsync -> Error adding article: {Environment.NewLine} {e.Message}");
                    return false; 
                    throw;
                }
            }

            return false;
        }
    }
}
