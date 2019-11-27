using System.Collections.Generic;
using System.ServiceModel.Syndication;
using System.Threading.Tasks;
using GoodNews.Data.Entities;

namespace GoodNews.Core
{
    public interface INewsParser
    {
        bool Add(Article article);
        Task<bool> AddNews(IEnumerable<Article> articles);
        IEnumerable<Article> GetNews(string url);
        string GetArticleText(string url);
        string GetArticleContent(string url);
        string GetThumbnail(SyndicationItem article);
        void Parse(string url);

    }
}
