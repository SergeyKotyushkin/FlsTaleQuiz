using System;
using System.Collections.Generic;
using System.Net.Mail;

namespace FlsTaleQuiz.Business.Interfaces
{
    public interface IMailGenerator
    {
        MailMessage Generate(IList<Tuple<string, string>> values, bool passed, string toEmail, int quantity);
    }
}