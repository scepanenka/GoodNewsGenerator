using System;
using GoodNews.Data.Entities;
using MediatR;

namespace GoodNews.CQS.Queries.GetArticleById
{
    public class GetArticleByIdQuery : IRequest<Article>
    {
        public Guid Id { get; }

        public GetArticleByIdQuery(Guid id)
        {
            Id = id;
        }
    }
}
