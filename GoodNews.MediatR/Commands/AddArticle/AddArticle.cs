using GoodNews.Data.Entities;
using MediatR;

namespace GoodNews.MediatR.Commands.AddArticle
{
    public class AddArticle : IRequest<bool>
    {
        public Article Article { get; }

        public AddArticle(Article article)
        {
            Article = article;
        }
    }
}
