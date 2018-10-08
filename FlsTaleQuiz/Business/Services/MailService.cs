using System;
using System.Net.Mail;
using FlsTaleQuiz.Business.Interfaces;

namespace FlsTaleQuiz.Business.Services
{
    public class MailService : IMailService
    {
        public bool Send(MailMessage mailMessage)
        {
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