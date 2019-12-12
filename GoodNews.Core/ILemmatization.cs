using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace GoodNews.Core
{
    public interface ILemmatization
    {
        Task<string> RequestLemmas(string input);
        string[] GetLemmas(string responseText);
    }
}
