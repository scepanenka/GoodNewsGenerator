using System;
using System.Collections.Generic;
using System.Text;
using MediatR;

namespace GoodNews.MediatR.Commands.DeleteComment
{
    public class DeleteComment : IRequest<bool>
    {
        public Guid Id { get; }

        public DeleteComment(Guid id)
        {
            Id = id;
        }
    }
}
