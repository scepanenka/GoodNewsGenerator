using System;
using System.Collections.Generic;
using System.Text;
using GoodNews.Data.Entities;
using MediatR;

namespace GoodNews.MediatR.Queries.GetSourceByUrl
{
    public class GetSourceByUrl : IRequest<Source>
    {
        public string Url { get; }
        public GetSourceByUrl(string url)
        {
            Url = url;
        }
    }
}
