using System.Collections.Generic;
using System.Linq;
using FlsTaleQuiz.Business.Interfaces;
using FlsTaleQuiz.Business.Models;

namespace FlsTaleQuiz.Business.Repositories
{
    public class QuestionAnswerLinkDtoRepositoryMock : IQuestionAnswerLinkDtoRepository
    {
        private const int QuestionAnswerLinksDtosCount = 100;

        public IEnumerable<QuestionAnswerLinkDto> GetAllByQuestionId(long questionId)
        {
            return QuestionAnswerLinkDtos().Where(qa => qa.QuestionId == questionId);
        }

        private List<QuestionAnswerLinkDto> QuestionAnswerLinkDtos()
        {
            var questionAnswerLinksDtosList = new List<QuestionAnswerLinkDto>();

            var answerId = 0;
            for (var i = 0; i < QuestionAnswerLinksDtosCount; i++)
            {
                questionAnswerLinksDtosList.Add(new QuestionAnswerLinkDto {QuestionId = i, AnswerId = answerId++});
                questionAnswerLinksDtosList.Add(new QuestionAnswerLinkDto {QuestionId = i, AnswerId = answerId++});
                questionAnswerLinksDtosList.Add(new QuestionAnswerLinkDto {QuestionId = i, AnswerId = answerId++});
                questionAnswerLinksDtosList.Add(new QuestionAnswerLinkDto {QuestionId = i, AnswerId = answerId++});
            }

            return questionAnswerLinksDtosList;
        }
    }
}