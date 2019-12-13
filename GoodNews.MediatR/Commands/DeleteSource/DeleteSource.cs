using System;
using GoodNews.Data.Entities;
using MediatR;

namespace GoodNews.MediatR.Commands.DeleteSource
{
    public class DeleteSource : IRequest<bool>
    {
        public DeleteSource(Source source)
        {
            Source = source;
        }

        public Source Source { get; }
    }
}
