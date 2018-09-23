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
        private JsonSerializerSettings JsonSerializerSettings =>
            new JsonSerializerSettings {ContractResolver = new CamelCasePropertyNamesContractResolver()};

        private readonly IAnswerRepository _answerRepository;

        public ResultController(IAnswerRepository answerRepository)
        {
            _answerRepository = answerRepository;
        }

        [HttpPost]
        public string SaveResults(string name, string phone, string email, UserAnswer[] userAnswers)
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

            var answersArray = answers.ToArray();

            var correctAnswers = answersArray.Where(a => a.IsValid).ToArray();
            var countOfCorrectAnswers = correctAnswers.Length;
            var result = new
            {
                correctQuestionsIds = correctAnswers.Select(a => a.QuestionId),
                questionsIds = answersArray.Select(a => a.QuestionId),
                CountOfCorrectAnswers = countOfCorrectAnswers,
                Constants.Settings.CountOfQuestions
            };

            return JsonConvert.SerializeObject(new {result}, JsonSerializerSettings);
        }
    }
}