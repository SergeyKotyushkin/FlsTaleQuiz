using System;
using System.Configuration;

namespace FlsTaleQuiz.Business.Constants
{
    public class Config
    {
        public static class Settings
        {
            public static int CountOfQuestions => Convert.ToByte(ConfigurationManager.AppSettings["NumberOfQuestionsInQuizSession"] ?? "8");
        }
    }
}