using System.Collections.Generic;
using GoodNews.Data.Entities;
using MediatR;

namespace GoodNews.MediatR.Queries.GetSources
{
    public class GetSources : IRequest<IEnumerable<Source>>
    {
    }
}
