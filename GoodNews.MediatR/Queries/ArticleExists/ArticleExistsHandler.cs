using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using GoodNews.Data;
using MediatR;

namespace GoodNews.MediatR.Queries.ArticleExists
{
    public class ArticleExistsHandler : IRequestHandler<ArticleExists, bool>
    {
        private readonly GoodNewsContext _context;

        public ArticleExistsHandler(GoodNewsContext context)
        {
            _context = context;
        }
        public async Task<bool> Handle(ArticleExists request, CancellationToken cancellationToken)
        {
            bool result = _context.News.Any(a => a.Url.Equals(request.Url));
            return await Task.FromResult(result);
        }
    }
}
