using System;
using System.Collections.Generic;
using System.Text;
using GoodNews.Data.Entities;
using MediatR;

namespace GoodNews.MediatR.Queries.GetSourceById
{
    public class GetSourceById :IRequest<Source>
    {
        public GetSourceById(Guid id)
        {
            Id = id;
        }

        public Guid Id { get; }


    }
}
