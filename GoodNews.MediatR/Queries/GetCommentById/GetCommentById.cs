using System;
using GoodNews.Data.Entities;
using MediatR;

namespace GoodNews.MediatR.Queries.GetCommentById
{
    public class GetCommentById : IRequest<Comment>
    {
        public Guid Id { get; }

        public GetCommentById(Guid id)
        {
            Id = id;
        }
    }
}
