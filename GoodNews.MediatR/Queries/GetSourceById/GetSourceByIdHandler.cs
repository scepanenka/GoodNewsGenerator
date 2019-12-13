using System.Threading;
using System.Threading.Tasks;
using GoodNews.Data;
using GoodNews.Data.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace GoodNews.MediatR.Queries.GetSourceById
{
    class GetSourceByIdHandler : IRequestHandler<GetSourceById, Source>
    {
        private readonly GoodNewsContext _context;

        public GetSourceByIdHandler(GoodNewsContext context)
        {
            _context = context;
        }

        public async Task<Source> Handle(GetSourceById request, CancellationToken cancellationToken)
        {
            var source = await _context.Sources.FirstOrDefaultAsync(n => n.Id.Equals(request.Id),
                cancellationToken: cancellationToken);
            return source;
        }
    }
}
