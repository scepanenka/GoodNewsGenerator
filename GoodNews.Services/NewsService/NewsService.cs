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
        private readonly ILemmatization _lemmatization;
        private readonly IAffinService _affin;

        public NewsService(IMediator mediator, IParser parser, ILemmatization lemmatization, IAffinService affin)
        {
            _mediator = mediator;
            _parser = parser;
            _lemmatization = lemmatization;
            _affin = affin;

        }

        public async Task<bool> AddNewsToDb()
        {
            try
            {
                List<Article> news = new List<Article>();
                // var tutByNews = await _parser.Parse(@"https://news.tut.by/rss/all.rss");
                // var s13Ru = await _parser.Parse(@"http://s13.ru/rss");
                var onlinerBy = await _parser.Parse(@"https://people.onliner.by/feed");
                // news.AddRange(tutByNews);
                // news.AddRange(s13Ru);
                news.AddRange(onlinerBy);

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

        public async Task<double> GetIndexPositivity(string articleText)
        {
            Dictionary<string, int> affinDictionary = await _affin.GetDictionary();
            try
            {
                var jsonLemma = await _lemmatization.GetLemmas(articleText);
                var articleWords = _lemmatization.GetDictionaryFromLemmas(jsonLemma);

                int totalScore = 0;
                int wordsCount = 0;
                foreach (var key in articleWords.Keys)
                {
                    if (affinDictionary.ContainsKey(key))
                    {
                        totalScore += affinDictionary[key] * articleWords[key];
                        wordsCount += articleWords[key];
                    }
                }

                double result = (wordsCount != 0) ? (double) totalScore / wordsCount : 0;
                return Math.Round(result, 2);
            }
            catch
            {
                return 0;
            }
        }
    }
}