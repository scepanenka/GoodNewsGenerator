using System.Collections.Generic;
using GoodNews.Data.Entities;
using MediatR;

namespace GoodNews.MediatR.Queries.GetNewsWithoutRating
{
    public class GetNewsWithoutRating : IRequest<IEnumerable<Article>>
    {
    }
}
