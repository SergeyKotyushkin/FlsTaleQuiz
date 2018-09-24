using System;
using System.Net.Mail;
using FlsTaleQuiz.Business.Interfaces;

namespace FlsTaleQuiz.Business.Services
{
    public class MailService : IMailService
    {
        public bool Send(string body, string subject, string toMail, string fromMail)
        {
            using (var mailMessage = new MailMessage())
            {
                mailMessage.From = new MailAddress(fromMail);
                mailMessage.To.Add(new MailAddress(toMail));
                mailMessage.Subject = subject;
                mailMessage.Body = body;
                mailMessage.IsBodyHtml = true;

                using (var client = new SmtpClient())
                {
                    try
                    {
                        client.Send(mailMessage);
                        return true;
                    }
                    catch (Exception exception)
                    {
                        return false;
                    }
                }
            }

        }
    }
}