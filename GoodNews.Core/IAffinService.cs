using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace GoodNews.Core
{
    public interface IAffinService
    {
        Task<Dictionary<string, int>> GetDictionary();
    }
}
