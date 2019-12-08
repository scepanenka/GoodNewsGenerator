using System;
using System.Collections.Generic;
using System.Text;
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
