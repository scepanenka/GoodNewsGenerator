
using System.Threading;
using System.Threading.Tasks;
using GoodNews.Data;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace GoodNews.MediatR.Commands.UpdateSource
{
    class UpdateSourceHandler : IRequestHandler<UpdateSource,bool>
    {
        private readonly GoodNewsContext _context;

        public UpdateSourceHandler(GoodNewsContext context)
        {
            _context = context;
        }

        public async Task<bool> Handle(UpdateSource request, CancellationToken cancellationToken)
        {
            _context.Entry(request.Source).State = EntityState.Modified;
            await _context.SaveChangesAsync(cancellationToken);
            return true;
        }
    }
}
