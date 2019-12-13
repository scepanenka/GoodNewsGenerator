using System.Threading;
using System.Threading.Tasks;
using GoodNews.Data;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace GoodNews.MediatR.Queries.SourceExists
{
    class SourceExistsHandler : IRequestHandler<SourceExists, bool>
    {
        private readonly GoodNewsContext _context;
        public async Task<bool> Handle(SourceExists request, CancellationToken cancellationToken)
        {
            bool result = await _context.Sources.AnyAsync(a => a.Id.Equals(request.Id), cancellationToken);
            return result;
        }
    }
}
