using System;
using System.Collections.Generic;
using System.Linq;
using FlsTaleQuiz.Business.Interfaces;
using FlsTaleQuiz.Business.Models;

namespace FlsTaleQuiz.Business.Repositories
{
    public class QuestionDtoRepositoryMock : IQuestionDtoRepository
    {
        private const int QuestionsDtosCount = 100;

        public QuestionDto GetRandom(IEnumerable<long> excludedQuestionsIds)
        {
            var excludedQuestionsIdsArray = excludedQuestionsIds == null ? new long[0] : excludedQuestionsIds.ToArray();

            var random = new Random();
            int index = random.Next(QuestionsDtosCount - excludedQuestionsIdsArray.Length - 1);
            return GetQuestionsDtos()[index];
        }

        public QuestionDto[] GetByIds(IEnumerable<long> questionsIds)
        {
            return GetQuestionsDtos().Where(q => questionsIds.Contains(q.Id)).ToArray();
        }

        private List<QuestionDto> GetQuestionsDtos()
        {
            var questionsDtosList = new List<QuestionDto>();

            for (var i = 0; i < QuestionsDtosCount; i++)
            {
                questionsDtosList.Add(new QuestionDto
                {
                    Id = i,
                    ImageUrl = "https://fakeimg.pl/350x200/?text=" + i,
                    Text = "Text " + i
                });
            }

            return questionsDtosList;
        }
    }
}