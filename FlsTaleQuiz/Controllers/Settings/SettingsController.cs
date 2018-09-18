using System.Web.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace FlsTaleQuiz.Controllers.Settings
{
    public class SettingsController : Controller
    {
        private JsonSerializerSettings JsonSerializerSettings =>
            new JsonSerializerSettings {ContractResolver = new CamelCasePropertyNamesContractResolver()};

        public string QuizOptions()
        {
            var settings = new
            {
                CountOfQuestions = 10
            };

            var labels = new
            {
                // intro component
                StartTestButtonLabel = "<Start test button>",

                // test component
                CurrentQuestionNumberLabelFormat = "<Question #{0} of {1}>",

                // question component
                AnswerButtonLabel = "<To answer>",

                // submit component
                FirstNameLabel = "<First Name>",
                LastNameLabel = "<Last Name>",
                EmailLabel = "<Email>",
                SubmitButtonLabel = "<Submit>",
                FirstNameRequiredMessage ="<First Name is required>",
                LastNameRequiredMessage ="<Last Name is required>",
                EmailRequiredMessage ="<Email is required>",
                EmailIncorrectMessage ="<Email is incorrect>",

                // finish component
                FinishText = "<Finish>"
            };

            return JsonConvert.SerializeObject(new {settings, labels}, JsonSerializerSettings);
        }
    }
}