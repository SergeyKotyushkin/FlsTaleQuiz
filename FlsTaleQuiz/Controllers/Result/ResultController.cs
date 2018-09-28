using System.Collections;
using System.Globalization;
using System.Linq;
using System.Web.Mvc;
using FlsTaleQuiz.Business.Constants;
using FlsTaleQuiz.Business.Interfaces;
using FlsTaleQuiz.Business.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace FlsTaleQuiz.Controllers.Result
{
    public class ResultController : Controller
    {
        private static JsonSerializerSettings JsonSerializerSettings =>
            new JsonSerializerSettings {ContractResolver = new CamelCasePropertyNamesContractResolver()};

        private readonly IAnswerRepository _answerRepository;
        private readonly IResultRepository _resultRepository;
        private readonly IMailService _mailService;

        public ResultController(
            IAnswerRepository answerRepository, 
            IResultRepository resultRepository,
            IMailService mailService)
        {
            _answerRepository = answerRepository;
            _resultRepository = resultRepository;
            _mailService = mailService;
        }

        [HttpPost]
        public string SaveResults(string email, string name, string phone, string comment, UserAnswer[] userAnswers)
        {
            email = email.Trim().ToLower(CultureInfo.GetCultureInfo("ru-RU"));
            if (userAnswers == null || userAnswers.Length == 0)
            {
                return JsonConvert.SerializeObject(new {HasErrors = true, MailSent = false}, JsonSerializerSettings);
            }

            string errorJson;
            if (!ValidateEmail(email, out errorJson))
            {
                return errorJson;
            }

            var answers = _answerRepository.GetByIds(userAnswers.Select(i => i.AnswerId).Distinct());
            if (answers == null)
            {
                return JsonConvert.SerializeObject(new {HasErrors = true, MailSent = false},
                    JsonSerializerSettings);
            }

            var answersArray = answers.ToArray();
            var correctAnswers = answersArray.Where(a => a.IsValid).ToArray();
            var countOfCorrectAnswers = correctAnswers.Length;

            if (!TrySendMail(email, countOfCorrectAnswers, out errorJson))
            {
                string trySaveResult;
                TrySaveResult($"{email}(FAILED ON SENDING EMAIL)", name, phone, comment, false, answersArray, countOfCorrectAnswers, out trySaveResult);

                return errorJson;
            }

            if (!TrySaveResult(email, name, phone, comment, true, answersArray, countOfCorrectAnswers, out errorJson))
            {
                return errorJson;
            }

            return JsonConvert.SerializeObject(new { }, JsonSerializerSettings);
        }

        private bool ValidateEmail(string email, out string errorJson)
        {
            errorJson = string.Empty;

            var emailCheck = _resultRepository.TestEmail(email);
            if (!emailCheck.HasValue)
            {
                errorJson =
                    JsonConvert.SerializeObject(new {HasErrors = true, MailSent = false}, JsonSerializerSettings);
                return false;
            }

            if (!emailCheck.Value)
            {
                errorJson = JsonConvert.SerializeObject(new {HasErrors = true, MailSent = false, UsedEmail = true},
                    JsonSerializerSettings);
                return false;
            }

            return true;
        }

        private bool TrySendMail(string email, int countOfCorrectAnswers, out string errorJson)
        {
            errorJson = string.Empty;

            var isEmailSent = _mailService.Send(
                $"Your results are: {countOfCorrectAnswers} correct answers of {Config.Settings.CountOfQuestions} questions",
                "FLS quiz",
                email,
                "fls@support.com");

            if (!isEmailSent)
            {
                errorJson = JsonConvert.SerializeObject(new {HasErrors = true, MailSent = false, MailSendError = true},
                    JsonSerializerSettings);
                return false;
            }

            return true;
        }

        private bool TrySaveResult(
            string email, 
            string name, 
            string phone, 
            string comment,
            bool emailSent,
            IEnumerable answersArray,
            int countOfCorrectAnswers, 
            out string errorJson)
        {
            errorJson = string.Empty;

            var saveResult = _resultRepository.SaveResult(
                email,
                JsonConvert.SerializeObject(new {answersArray}, JsonSerializerSettings),
                countOfCorrectAnswers,
                Config.Settings.CountOfQuestions,
                emailSent,
                name ?? string.Empty,
                phone ?? string.Empty,
                comment ?? string.Empty);

            if (!saveResult)
            {
                errorJson = JsonConvert.SerializeObject(new {HasErrors = true, MailSent = true},
                    JsonSerializerSettings);
                return false;
            }

            return true;
        }
    }
}