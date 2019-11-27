using System;
using System.Collections.Generic;
using System.ServiceModel.Syndication;
using System.Threading.Tasks;
using GoodNews.Core;
using GoodNews.Data.Entities;
using GoodNews.MediatR.Commands.AddNews;
using MediatR;

namespace GoodNews.ApiServices.Parsers
{
    public class NewsParser : INewsParser
    {
        private readonly IMediator _mediator;

        public NewsParser(IMediator mediator)
        {
            _mediator = mediator;
        }
        public async void Parse(string url)
        {
            await AddNews(GetNews(url));
        }

        public async Task<bool> AddNews(IEnumerable<Article> news)
        {
            if (news != null)
            {
                    await _mediator.Send(new AddNewsAsync(news));
                    return true;
            }
            return false;
        }

        public bool Add(Article article)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Article> GetNews(string url)
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

        public string GetThumbnail(SyndicationItem article)
        {
            throw new NotImplementedException();
        }
    }
}
