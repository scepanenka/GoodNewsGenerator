using System.Collections.Generic;
using System.Threading.Tasks;
using GoodNews.Data.Entities;

namespace GoodNews.Core
{
    public interface INewsService
    {
        Task<bool> Run();
    }
}
