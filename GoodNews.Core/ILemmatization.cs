using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace GoodNews.Core
{
    public interface ILemmatization
    {
        Task<string> GetLemmas(string input);
        Dictionary<string, int> GetDictionaryFromLemmas(string responseText);
    }
}
