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
        IEnumerable<Article> GetFromUrl(string url);
        bool Add(Article article);
        bool AddRange(IEnumerable<Article> articles);
    }
}
