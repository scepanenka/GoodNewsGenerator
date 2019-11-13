using System.Collections.Generic;
using GoodNews.Data.Entities;

namespace Core
{
    public interface INewsParser
    {
        bool Add(Article article);
        bool AddNews(IEnumerable<Article> articles);

        IEnumerable<Article> GetFromRss();

        string GetTextOfArticle(string url);
        
        void Parse();

    }
}
