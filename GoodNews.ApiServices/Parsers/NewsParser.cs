using System;
using System.Collections.Generic;
using System.ServiceModel.Syndication;
using GoodNews.Core;
using GoodNews.Data.Entities;
using MediatR;

namespace GoodNews.ApiServices.Parsers
{
    public class NewsParser : INewsParser
    {
        private readonly IMediator _mediator;

        NewsParser(IMediator mediator)
        {
            _mediator = mediator;
        }
        public bool Add(Article article)
        {
            throw new NotImplementedException();
        }

        public bool AddNews(IEnumerable<Article> articles)
        {
            throw new NotImplementedException();
        }

        public string GetArticleContent(string url)
        {
            throw new NotImplementedException();
        }

        public string GetArticleText(string url)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Article> GetNews()
        {
            throw new NotImplementedException();
        }

        public string GetThumbnail(SyndicationItem article)
        {
            throw new NotImplementedException();
        }

        public void Parse()
        {
            throw new NotImplementedException();
        }
    }
}
