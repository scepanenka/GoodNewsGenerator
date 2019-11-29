using System.Collections.Generic;
using System.ServiceModel.Syndication;
using System.Threading.Tasks;
using GoodNews.Data.Entities;

namespace GoodNews.Core
{
    public interface INewsParser
    {
        void AddNews(IEnumerable<Article> articles);
        Task<IEnumerable<Article>> GetNewsAsync(string url);
        void Parse(string url);

    }
}
