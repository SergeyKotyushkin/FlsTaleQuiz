namespace FlsTaleQuiz.Business.Interfaces
{
    public interface IMailService
    {
        bool Send(string body, string subject, string toMail, string fromMail);
    }
}