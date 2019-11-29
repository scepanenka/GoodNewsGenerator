using System;
using System.Collections.Generic;
using System.Text;
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
            var source = await _context.Sources.FirstOrDefaultAsync(n => n.Url.Equals(request.Url),
                cancellationToken: cancellationToken);
            return source;
        }
    }
}
