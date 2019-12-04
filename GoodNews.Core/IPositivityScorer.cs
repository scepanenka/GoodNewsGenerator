using System;
using System.Collections.Generic;
using System.Text;

namespace GoodNews.Core
{
    public interface IPositityScorer
    {
        float GetPositivity(string text);
    }
}
