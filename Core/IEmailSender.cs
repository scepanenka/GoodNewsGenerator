using System.Collections.Generic;
using System.Threading.Tasks;

namespace Core
{
    public interface IEmailSender
    {
        bool SendEmail(string subject, string body, IEnumerable<string> to, IEnumerable<string> toCc);
    }
}
