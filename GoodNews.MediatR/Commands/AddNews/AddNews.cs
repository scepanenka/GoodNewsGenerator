using System.Collections.Generic;
using GoodNews.Data.Entities;
using MediatR;

namespace GoodNews.MediatR.Commands.AddNews
{
    public class AddNews: IRequest<bool>
    {
        public IEnumerable<Article> News { get; }

        public AddNews(IEnumerable<Article> news)
        {
            News = news;
        }
    }
}
