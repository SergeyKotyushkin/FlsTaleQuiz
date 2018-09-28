using System.Web.Mvc;
using FlsTaleQuiz.Business.Constants;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace FlsTaleQuiz.Controllers.Settings
{
    public class SettingsController : Controller
    {
        private static JsonSerializerSettings JsonSerializerSettings =>
            new JsonSerializerSettings {ContractResolver = new CamelCasePropertyNamesContractResolver()};

        public string QuizOptions()
        {
            var settings = new
            {
                Config.Settings.CountOfQuestions
            };

            return JsonConvert.SerializeObject(new {settings}, JsonSerializerSettings);
        }
    }
}