using System;
using GoodNews.Data.Entities;
using MediatR;

namespace GoodNews.MediatR.Queries.GetArticleById
{
    public class GetArticleById : IRequest<Article>
    {
        public Guid Id { get; }

        public GetArticleById(Guid id)
        {
            Id = id;
        }
    }
}
