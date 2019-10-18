using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
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
