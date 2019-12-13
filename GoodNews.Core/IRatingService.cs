using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using GoodNews.Data.Entities;

namespace GoodNews.Core
{
    public interface IRatingService
    {
        Task<IEnumerable<Article>> GetUnratedFromDb();
        Task<IEnumerable<Article>> ScoreNewsRatings(IEnumerable<Article> news);
        Task<double> ScoreArticleRating(string articleText);
        Task<bool> SaveRatingsToDB(IEnumerable<Article> news);
    }
}
