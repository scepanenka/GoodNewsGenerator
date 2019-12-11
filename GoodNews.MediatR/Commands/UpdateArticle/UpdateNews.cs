using System.Collections.Generic;
using GoodNews.Data.Entities;
using MediatR;

namespace GoodNews.MediatR.Commands.UpdateArticle
{
    public class UpdateNews : IRequest<bool>
        {
        public IEnumerable<Article> News { get; }

        public UpdateNews(IEnumerable<Article> news)
        {
            News = news;
        }
    }
}