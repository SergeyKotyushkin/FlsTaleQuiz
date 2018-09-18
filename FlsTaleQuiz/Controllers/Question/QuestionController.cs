using System.Web.Mvc;
using FlsTaleQuiz.Business.Interfaces;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace FlsTaleQuiz.Controllers.Question
{
    public class QuestionController : Controller
    {
        private JsonSerializerSettings JsonSerializerSettings =>
            new JsonSerializerSettings {ContractResolver = new CamelCasePropertyNamesContractResolver()};

        private readonly IQuestionRepository _questionRepository;

        public QuestionController(IQuestionRepository questionRepository)
        {
            _questionRepository = questionRepository;
        }

        [HttpPost]
        public string GetRandom(long[] excludedQuestionsIds)
        {
            var question = _questionRepository.GetRandom(excludedQuestionsIds);

            return JsonConvert.SerializeObject(new {question}, JsonSerializerSettings);
        }
    }
}