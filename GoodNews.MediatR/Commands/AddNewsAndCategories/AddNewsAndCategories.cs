using System.Collections.Generic;
using GoodNews.Data.Entities;
using MediatR;

namespace GoodNews.MediatR.Commands.AddNewsAndCategories
{
    public class AddNewsAndCategories : IRequest<bool>
    {
        public IEnumerable<Article> News { get; }

        public AddNewsAndCategories(IEnumerable<Article> news)
        {
            News = news;
        }
    }
}
