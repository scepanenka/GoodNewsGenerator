using System;
using System.Collections.Generic;
using System.Text;
using GoodNews.Data.Entities;
using MediatR;

namespace GoodNews.MediatR.Commands.AddSource
{
    public class AddSource : IRequest<Guid>
    {
        public Source Source { get; }

        public AddSource(Source source)
        {
            Source = source;
        }
    }
}
