using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using GoodNews.Core;
using GoodNews.Data.Entities;
using GoodNews.MediatR.Commands.UpdateArticle;
using GoodNews.MediatR.Queries.GetNewsWithoutRating;
using MediatR;

namespace SentimentRatingService
{
    public class SentimentRatingService
    {
        private readonly IMediator _mediator;
        private readonly ILemmatization _lemmatization;
        private readonly IAffinService _affin;


        public SentimentRatingService(IMediator mediator, ILemmatization lemmatization, IAffinService affin)
        {
            _mediator = mediator;
            _lemmatization = lemmatization;
            _affin = affin;
        }

        public async Task<bool> SaveScoredRatingsToDB(IEnumerable<Article> news)
        {
            return await _mediator.Send(new UpdateNews(news));
        }

        public async Task<IEnumerable<Article>> GetUnratedNews()
        {
            IEnumerable<Article> news = await _mediator.Send(new GetNewsWithoutRating());
            return news;
        }

        public async Task<IEnumerable<Article>> ScoreUnscoredRatings(IEnumerable<Article> news)
        {
            foreach (var article in news)
            {
                double rating = await ScoreArticleRating(article.Text);
                article.SentimentRating = rating;
            }

            return news;
        }

        private async Task<double> ScoreArticleRating(string articleText)
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
