﻿using System.Web.Mvc;
using FlsTaleQuiz.Business.Interfaces;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace FlsTaleQuiz.Controllers.Question
{
    public class QuestionController : Controller
    {
        private static JsonSerializerSettings JsonSerializerSettings =>
            new JsonSerializerSettings {ContractResolver = new CamelCasePropertyNamesContractResolver()};

        private readonly IQuestionRepository _questionRepository;

        public QuestionController(IQuestionRepository questionRepository)
        {
            _questionRepository = questionRepository;
        }

        [HttpPost]
        public string GetRandom(int[] excludedQuestionsIds)
        {
            var question = _questionRepository.GetRandom(excludedQuestionsIds);

            return question == null
                ? JsonConvert.SerializeObject(new {HasErrors = true}, JsonSerializerSettings)
                : JsonConvert.SerializeObject(new {Question = HideValidAnswer(question)}, JsonSerializerSettings);
        }

        private static Business.Models.Question HideValidAnswer(Business.Models.Question question)
        {
            question.Answers.ForEach(a => a.IsValid = false);
            return question;
        }
    }
}