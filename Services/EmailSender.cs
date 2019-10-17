using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Threading;
using System.Threading.Tasks;
using Core;

namespace SmtpEmailService
{
    public class EmailSender : IEmailSender
    {
        private const string HOST = "smtp.gmail.com";
        private const int PORT = 587;
        private const string OWNER_EMAIL = "myusername@gmail.com";
        private const string OWNER_PASSWORD = "pswd";
        private const string FROM = "myusername@gmail.com";


        public async Task<bool> SendEmail(string subject, string body, IEnumerable<string> to, IEnumerable<string> toCc)
        {
            try
            {
                var client = new SmtpClient(HOST, PORT)
                {
                    Credentials = new NetworkCredential(OWNER_EMAIL, OWNER_PASSWORD),
                    EnableSsl = true
                };
                client.Send(FROM, string.Join(", ", to), subject, body);

                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }
    }
}