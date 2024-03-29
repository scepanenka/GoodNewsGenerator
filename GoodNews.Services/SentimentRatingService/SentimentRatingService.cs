﻿using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using GoodNews.Core;
using GoodNews.Data.Entities;
using GoodNews.MediatR.Commands.UpdateArticle;
using GoodNews.MediatR.Queries.GetNewsWithoutRating;
using MediatR;

namespace SentimentRatingService
{
    public class SentimentRatingService : IRatingService
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

        public async Task<bool> SaveRatingsToDB(IEnumerable<Article> news)
        {
            return await _mediator.Send(new UpdateNews(news));
        }

        public async Task<IEnumerable<Article>> GetUnratedFromDb()
        {
            IEnumerable<Article> news = await _mediator.Send(new GetNewsWithoutRating());
            return news;
        }

        public async Task<IEnumerable<Article>> ScoreNewsRatings(IEnumerable<Article> news)
        {
            foreach (var article in news)
            {
                double rating = await ScoreArticleRating(article.Text);
                article.SentimentRating = rating;
            }

            return news;
        }

        public async Task<double> ScoreArticleRating(string articleText)
        {
            Dictionary<string, int> affinDictionary = await _affin.GetDictionary();
            try
            {
                var jsonLemma = await _lemmatization.RequestLemmas(articleText);
                var lemmas = _lemmatization.GetLemmas(jsonLemma);

                int totalScore = 0;
                int wordsCount = 0;

                for (var i = 0; i < lemmas.Length; i++)
                {
                    string lemma = lemmas[i];
                    if (lemma != string.Empty)
                    {
                        if (affinDictionary.ContainsKey(lemma))
                        {
                            wordsCount++;
                            totalScore += affinDictionary[lemma];
                        }
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
