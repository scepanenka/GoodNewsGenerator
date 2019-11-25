using System;
using System.Collections.Generic;
using System.Text;
using GoodNews.Data.Entities;
using MediatR;

namespace GoodNews.MediatR.Queries.GetCommentsByArticleId
{
    class GetCommentsByArticleIdQuery : IRequest<IEnumerable<Comment>>
    {
        public Guid ArticleId { get; }

        public GetCommentsByArticleIdQuery(Guid id)
        {
            ArticleId = id;
        }
    }
}
