using System;
using GoodNews.Data.Entities;
using MediatR;

namespace GoodNews.MediatR.Commands.AddComment
{
    public class AddComment : IRequest<Guid>
    {
        public Comment Comment { get; }

        public AddComment (Comment comment)
        {
            Comment = comment;
        }
    }
}
