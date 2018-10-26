using System;
using System.Collections.Generic;
using System.Net.Mail;
using FlsTaleQuiz.Business.Constants;

namespace FlsTaleQuiz.Business.Interfaces
{
    public interface IMailGenerator
    {
        MailMessage Generate(IList<Tuple<string, string>> values, PassGrade grade, string toEmail, int quantity);
    }
}