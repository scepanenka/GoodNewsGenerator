using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using GoodNews.Data;
using GoodNews.Data.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace GoodNews.MediatR.Queries.GetSourceByUrl
{
    public class GetSourceByUrlHandler : IRequestHandler<GetSourceByUrl, Source>
    {
        private readonly GoodNewsContext _context;
        public GetSourceByUrlHandler(GoodNewsContext context)
        {
            _context = context;
        }

        public async Task<Source> Handle(GetSourceByUrl request, CancellationToken cancellationToken)
        {
            var source = _context.Sources.FirstOrDefault(n => n.Url.Equals(request.Url));
            return await Task.FromResult(source);
        }
    }
}
