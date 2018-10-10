using System.Net.Mail;

namespace FlsTaleQuiz.Business.Interfaces
{
    public interface IMailService
    {
        bool Send(MailMessage mailMessage);
    }
}