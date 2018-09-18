using System.Collections.Generic;
using FlsTaleQuiz.Business.Models;

namespace FlsTaleQuiz.Business.Interfaces
{
    public interface IQuestionAnswerLinkDtoRepository
    {
        IEnumerable<QuestionAnswerLinkDto> GetAllByQuestionId(long questionId);
    }
}
