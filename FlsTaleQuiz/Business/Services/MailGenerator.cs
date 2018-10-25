using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Web.Hosting;
using FlsTaleQuiz.Business.Constants;
using FlsTaleQuiz.Business.Interfaces;

namespace FlsTaleQuiz.Business.Services
{
    public class MailGenerator : IMailGenerator
    {
        public MailMessage Generate(IList<Tuple<string, string>> values, bool passed, string toEmail, int quantity)
        {
            var variationIndex = SelectWordVariationIndex(quantity);
            var otvet = MailGenerator.Otvet[variationIndex];
            var pravilnyj = MailGenerator.Pravilnyj[variationIndex];
            var vernyj = MailGenerator.Vernyj[variationIndex];

            var extendedValuesList = values.Concat(new[]
            {
                Tuple.Create("%%форма_слова_ответ%%", otvet),
                Tuple.Create("%%форма_слова_правильный%%", pravilnyj),
                Tuple.Create("%%форма_слова_верный%%", vernyj)
            }).ToList();

            var mailMessage = new MailMessage
            {
                Subject = InjectValues(LoadEmailSubjectTemplate(passed), extendedValuesList),
                Body = InjectValues(LoadEmailTemplate(passed), extendedValuesList),
                IsBodyHtml = true
            };
            mailMessage.To.Add(new MailAddress(toEmail));
            mailMessage.ReplyToList.Add(Config.Settings.ReplyTo);
            return mailMessage;
        }

        private static string LoadEmailSubjectTemplate(bool passed)
        {
            var templateFileName = passed
                ? "/App_Data/PassedEmailSubjectTemplate.txt"
                : "/App_Data/FailedEmailSubjectTemplate.txt";

            return File.ReadAllText(HostingEnvironment.MapPath(templateFileName));
        }

        private static string LoadEmailTemplate(bool passed)
        {
            var templateFileName = passed
                ? "/App_Data/PassedEmailTemplate.html"
                : "/App_Data/FailedEmailTemplate.html";

            return File.ReadAllText(HostingEnvironment.MapPath(templateFileName));
        }

        private static string InjectValues(string template, IEnumerable<Tuple<string, string>> values)
        {
            var sb = new StringBuilder(template);
            foreach (var value in values)
            {
                sb.Replace(value.Item1, value.Item2);
            }

            return sb.ToString();
        }

        private static readonly string[] Otvet = {"ответ", "ответа", "ответов"};
        private static readonly string[] Vernyj = {"верный", "верных", "верных"};
        private static readonly string[] Pravilnyj = {"правильный", "правильных", "правильных"};

        private static int SelectWordVariationIndex(int quantity)
        {
            var quantityUnder100 = quantity % 100;
            var quantityUnder10 = quantityUnder100 % 10;
            int variationIndex;

            if (quantityUnder100 >= 11 && quantityUnder100 <= 19)
                variationIndex = 2;
            else if (quantityUnder10 == 1)
                variationIndex = 0;
            else if (quantityUnder10 >= 2 && quantityUnder10 <= 4)
                variationIndex = 1;
            else
                variationIndex = 2;

            return variationIndex;
        }
    }
}