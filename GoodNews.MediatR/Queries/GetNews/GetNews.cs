using System;
using System.Collections.Generic;
using GoodNews.Data.Entities;
using MediatR;

namespace GoodNews.MediatR.Queries.GetNews
{
    public class GetNews : IRequest<IEnumerable<Article>>
    {
        
    }
}
