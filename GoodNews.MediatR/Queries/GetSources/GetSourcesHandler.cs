using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using GoodNews.Data;
using GoodNews.Data.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace GoodNews.MediatR.Queries.GetSources
{
    public class GetSourcesHandler : IRequestHandler<GetSources, IEnumerable<Source>>
    {
        private readonly GoodNewsContext _context;

        public GetSourcesHandler(GoodNewsContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<Source>> Handle(GetSources request, CancellationToken cancellationToken)
        {
            var sources = await _context.Sources.ToListAsync(cancellationToken);
            return sources;
        }
    }
}
