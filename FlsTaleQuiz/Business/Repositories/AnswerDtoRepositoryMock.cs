using System.Collections.Generic;
using System.Linq;
using FlsTaleQuiz.Business.Interfaces;
using FlsTaleQuiz.Business.Models;

namespace FlsTaleQuiz.Business.Repositories
{
    public class AnswerDtoRepositoryMock : IAnswerDtoRepository
    {
        private const int AnswersDtosCount = 400;
        
        public IEnumerable<AnswerDto> GetByIds(IEnumerable<long> answersIds)
        {
            return GetAnswersDtos().Where(a => answersIds.Contains(a.Id));
        }

        private static IEnumerable<AnswerDto> GetAnswersDtos()
        {
            var answersDtosList = new List<AnswerDto>();

            for (var i = 0; i < AnswersDtosCount; i++)
            {
                answersDtosList.Add(new AnswerDto {Id = i, Text = "Answer " + i, IsRight = i % 4 == 0});
            }

            return answersDtosList;
        }
    }
}