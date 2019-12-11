using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using GoodNews.Core;
using GoodNews.Data.Entities;
using GoodNews.MediatR.Commands.AddNews;
using MediatR;

namespace GoodNews.NewsService
{
    public class NewsService : INewsService
    {
        private readonly IMediator _mediator;
        private readonly IParser _parser;

        public NewsService(IMediator mediator, IParser parser)
        {
            _mediator = mediator;
            _parser = parser;
        }

        public async Task<bool> AddNewsToDb()
        {
            try
            {
                List<Article> news = new List<Article>();
                // var tutByNews = await _parser.Parse(@"https://news.tut.by/rss/all.rss");
                var s13Ru = await _parser.Parse(@"http://s13.ru/rss");
                // var onlinerBy = await _parser.Parse(@"https://people.onliner.by/feed");
                // news.AddRange(tutByNews);
                // news.AddRange(s13Ru);
                news.AddRange(s13Ru);

                await _mediator.Send(new AddNews(news));
                return true;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return false;
                throw;
            }
        }
    }
}