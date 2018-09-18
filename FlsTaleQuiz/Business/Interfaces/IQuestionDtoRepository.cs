using System.Collections.Generic;
using FlsTaleQuiz.Business.Models;

namespace FlsTaleQuiz.Business.Interfaces
{
    public interface IQuestionDtoRepository
    {
        QuestionDto GetRandom(IEnumerable<long> excludedQuestionsIds);
    }
}
