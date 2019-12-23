using System;
using System.Collections.Generic;
using System.Text;
using GoodNews.Data.Entities;
using MediatR;

namespace GoodNews.MediatR.Queries.GetNewsByPage
{
    public class GetNewsByPage : IRequest<IEnumerable<Article>>
    {
        public int Page { get; }
        public GetNewsByPage(int page)
        {
            Page = page;
        }
    }
}