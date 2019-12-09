using MediatR;

namespace GoodNews.MediatR.Queries.ArticleExists
{
    public class ArticleExists : IRequest<bool>
    {
        public string Url { get; }

        public ArticleExists(string url)
        {
            Url = url;
        }
    }
}
