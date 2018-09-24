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

        public ResultController(IAnswerRepository answerRepository, IResultRepository resultRepository)
        {
            _answerRepository = answerRepository;
            _resultRepository = resultRepository;
        }

        [HttpPost]
        public string SaveResults(string email, string name, string phone, string comment, UserAnswer[] userAnswers)
        {
            if (userAnswers == null || userAnswers.Length == 0)
            {
                return JsonConvert.SerializeObject(new {HasErrors = true, MailSent = false}, JsonSerializerSettings);
            }

            var answers = _answerRepository.GetByIds(userAnswers.Select(i => i.AnswerId).Distinct());
            if (answers == null)
            {
                return JsonConvert.SerializeObject(new {HasErrors = true, MailSent = false},
                    JsonSerializerSettings);
            }

            var emailCheck = _resultRepository.TestEmail(email);
            if (!emailCheck.HasValue)
            {
                return JsonConvert.SerializeObject(new {HasErrors = true, MailSent = false}, JsonSerializerSettings);
            }

            if (!emailCheck.Value)
            {
                return JsonConvert.SerializeObject(new {HasErrors = true, MailSent = false, UsedEmail = true},
                    JsonSerializerSettings);
            }

            var answersArray = answers.ToArray();
            var correctAnswers = answersArray.Where(a => a.IsValid).ToArray();
            var countOfCorrectAnswers = correctAnswers.Length;

            var saveResult = _resultRepository.SaveResult(
                email,
                JsonConvert.SerializeObject(new {answersArray}, JsonSerializerSettings),
                countOfCorrectAnswers,
                Constants.Settings.CountOfQuestions,
                true,
                name ?? string.Empty,
                phone ?? string.Empty,
                comment ?? string.Empty);

            if (saveResult)
            {
                return JsonConvert.SerializeObject(new { }, JsonSerializerSettings);
            }

            return JsonConvert.SerializeObject(new {HasErrors = true, MailSent = true}, JsonSerializerSettings);
        }
    }
}