using System.Collections.Generic;
using System.Threading.Tasks;
using GoodNews.Data.Entities;

namespace GoodNews.Core
{
    public interface INewsParser
    {
        Task AddNews(IEnumerable<Article> articles);
        Task<IEnumerable<Article>> GetNewsAsync(string url);
        Task Parse(string url);

    }
}
