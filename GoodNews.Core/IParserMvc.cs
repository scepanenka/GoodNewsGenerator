using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace GoodNews.Core
{
    public interface IParserMvc
    {
        Task Parse(string url);
    }
}
