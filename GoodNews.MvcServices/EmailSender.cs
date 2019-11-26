using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Mail;
using GoodNews.Core;

namespace GoodNews.MvcServices
{
    public class EmailSender : IEmailSender
    {
        private const string HOST = "smtp.gmail.com";
        private const int PORT = 587;
        private const string OWNER_EMAIL = "adm.goodnews@gmail.com";
        private const string OWNER_PASSWORD = "Qwe1234!";
        private const string FROM = "GoodNews admin";


        public bool SendEmail(string subject, string body, IEnumerable<string> to, IEnumerable<string> toCc)
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