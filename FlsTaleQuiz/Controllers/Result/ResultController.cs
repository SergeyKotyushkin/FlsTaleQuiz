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

        private readonly IAnswerDtoRepository _answerDtoRepository;

        public ResultController(IAnswerDtoRepository answerDtoRepository)
        {
            _answerDtoRepository = answerDtoRepository;
        }

        [HttpPost]
        public string SaveResults(string firstName, string lastName, string email, UserAnswer[] userAnswers)
        {
            if (userAnswers != null && userAnswers.Length != 0)
            {
                var answersDtos = _answerDtoRepository.GetByIds(userAnswers.Select(i => i.AnswerId).Distinct());
                var correctAnswers = answersDtos.Where(a => a.IsRight).ToArray();
                var correctAnswersCount = correctAnswers.Length;
                var questionsCount = Constants.Settings.CountOfQuestions;
                var correctUserAnswers = userAnswers.Where(i => correctAnswers.Any(a => a.Id == i.AnswerId)).ToArray();
                return JsonConvert.SerializeObject(
                    new {firstName, lastName, email, userAnswers, correctUserAnswers, correctAnswersCount, questionsCount},
                    JsonSerializerSettings);
            }

            return JsonConvert.SerializeObject(new {Constants.Labels.ErrorMessage}, JsonSerializerSettings);
        }
    }
}