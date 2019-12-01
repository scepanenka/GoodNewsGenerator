using System.Threading.Tasks;

namespace GoodNews.Core
{
    public interface IParser
    {
        Task Parse(string url);
    }
}
