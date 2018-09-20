using System.Web.Mvc;
using FlsTaleQuiz.Business.Constants;
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
                Constants.Settings.CountOfQuestions
            };

            var labels = new
            {
                // intro component
                Constants.Labels.StartTestButtonLabel,

                // test component
                Constants.Labels.CurrentQuestionNumberLabelFormat,

                // question component
                Constants.Labels.AnswerButtonLabel,

                // submit component
                Constants.Labels.FirstNameLabel,
                Constants.Labels.LastNameLabel,
                Constants.Labels.EmailLabel,
                Constants.Labels.SubmitButtonLabel,
                Constants.Labels.FirstNameRequiredMessage,
                Constants.Labels.LastNameRequiredMessage,
                Constants.Labels.EmailRequiredMessage,
                Constants.Labels.EmailIncorrectMessage,

                // finish component
                Constants.Labels.FinishText
            };

            return JsonConvert.SerializeObject(new {settings, labels}, JsonSerializerSettings);
        }
    }
}