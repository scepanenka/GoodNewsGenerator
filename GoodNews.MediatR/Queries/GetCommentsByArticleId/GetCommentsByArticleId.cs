using System;
using System.Collections.Generic;
using GoodNews.Data.Entities;
using MediatR;

namespace GoodNews.MediatR.Queries.GetCommentsByArticleId
{
    class GetCommentsByArticleId : IRequest<IEnumerable<Comment>>
    {
        public Guid ArticleId { get; }

        public GetCommentsByArticleId(Guid id)
        {
            ArticleId = id;
        }
    }
}
