using System.Collections.Generic;

namespace GoodNews.Core
{
    public interface IEmailSender
    {
        bool SendEmail(string subject, string body, IEnumerable<string> to, IEnumerable<string> toCc);
    }
}
