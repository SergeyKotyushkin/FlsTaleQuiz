using System.Web.Mvc;
using FlsTaleQuiz.Business.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace FlsTaleQuiz.Controllers.Result
{
    public class ResultController : Controller
    {
        private JsonSerializerSettings JsonSerializerSettings =>
            new JsonSerializerSettings {ContractResolver = new CamelCasePropertyNamesContractResolver()};

        [HttpPost]
        public string SaveResults(string firstName, string lastName, string email, UserAnswer[] userAnswers)
        {
            return JsonConvert.SerializeObject(new {firstName, lastName, email, userAnswers}, JsonSerializerSettings);
        }
    }
}