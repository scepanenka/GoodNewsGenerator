using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Core
{
    public interface IEmailSender
    {
        Task<bool> SendEmail(string subject, string body, IEnumerable<string> to, IEnumerable<string> toCc);
    }
}
