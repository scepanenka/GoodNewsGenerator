using System.Collections.Generic;
using GoodNews.Data.Entities;

namespace GoodNews.Core
{
    public interface INewsParser
    {
        bool Add(Article article);
        bool AddNews(IEnumerable<Article> articles);

        IEnumerable<Article> GetNews();
        string GetArticleText(string url);
        string GetArticleContent(string url);
        
        void Parse();

    }
}
