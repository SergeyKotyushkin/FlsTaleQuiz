namespace FlsTaleQuiz.Business.Constants
{
    public class Constants
    {
        public static class Labels
        {
            // intro component
            public static string StartTestButtonLabel = "<Start test button>";

            // test component
            public static string CurrentQuestionNumberLabelFormat = "<Question #{0} of {1}>";

            // question component
            public static string AnswerButtonLabel = "<To answer>";

            // submit component
            public static string FirstNameLabel = "<First Name>";
            public static string LastNameLabel = "<Last Name>";
            public static string EmailLabel = "<Email>";
            public static string SubmitButtonLabel = "<Submit>";
            public static string FirstNameRequiredMessage = "<First Name is required>";
            public static string LastNameRequiredMessage = "<Last Name is required>";
            public static string EmailRequiredMessage = "<Email is required>";
            public static string EmailIncorrectMessage = "<Email is incorrect>";

            // finish component
            public static string FinishText = "<Finish>";

            // Common
            public static string ErrorMessage = "<Error>";
        }

        public static class Settings
        {
            public static int CountOfQuestions = 3;
        }
    }
}