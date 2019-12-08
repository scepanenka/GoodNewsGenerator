using System.Threading.Tasks;

namespace GoodNews.Core
{
    public interface IPositivityScorer
    {
        Task<double> GetIndexPositivity(string text);
    }
}
