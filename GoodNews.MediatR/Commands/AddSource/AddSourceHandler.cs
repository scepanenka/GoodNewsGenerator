using System;
using System.Threading;
using System.Threading.Tasks;
using GoodNews.Data;
using MediatR;

namespace GoodNews.MediatR.Commands.AddSource
{
    public class AddSourceHandler : IRequestHandler<AddSource, Guid>
    {
        private readonly GoodNewsContext _context;

        public AddSourceHandler(GoodNewsContext context)
        {
            _context = context;
        }

        public async Task<Guid> Handle(AddSource request, CancellationToken cancellationToken)
        {
            await _context.Sources.AddAsync(request.Source, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);
            return request.Source.Id;
        }
    }
}
