using System;
using System.Threading;
using System.Threading.Tasks;
using GoodNews.Data;
using MediatR;
using Serilog;

namespace GoodNews.MediatR.Commands.DeleteSource
{
    public class DeleteSourceHandler : IRequestHandler<DeleteSource,bool>
    {
        private readonly GoodNewsContext _context;

        public DeleteSourceHandler(GoodNewsContext context)
        {
            _context = context;
        }

        public async Task<bool> Handle(DeleteSource request, CancellationToken cancellationToken)
        {
            try
            {
                _context.Sources.Remove(request.Source);
                await _context.SaveChangesAsync(cancellationToken);
                Log.Information($"Source '{request.Source.Name}' deleted");
                return true;
            }
            catch (Exception e)
            {
                Log.Error("Error while delete source");
                return false;
            }
            
        }
    }
}
