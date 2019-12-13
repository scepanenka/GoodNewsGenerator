using System;
using System.Collections.Generic;
using System.Text;
using MediatR;

namespace GoodNews.MediatR.Queries.SourceExists
{
    public class SourceExists : IRequest<bool>
    {
        public SourceExists(Guid id)
        {
            Id = id;
        }

        public Guid Id { get; }
    }
}
