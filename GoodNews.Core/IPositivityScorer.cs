using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace GoodNews.Core
{
    public interface IPositivityScorer
    {
        Task<float> GetIndexPositivity(string text);
    }
}
