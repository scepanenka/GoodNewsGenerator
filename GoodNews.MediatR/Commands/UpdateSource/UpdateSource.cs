using System;
using GoodNews.Data.Entities;
using MediatR;

namespace GoodNews.MediatR.Commands.UpdateSource
{
    public class UpdateSource : IRequest<bool>
    {
        public Source Source { get; }
        public UpdateSource(Source source)
        {
            Source = source;
        }
    }
}
