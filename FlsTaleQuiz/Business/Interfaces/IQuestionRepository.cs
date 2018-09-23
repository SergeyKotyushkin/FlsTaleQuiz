using System.Collections.Generic;
using FlsTaleQuiz.Business.Models;

namespace FlsTaleQuiz.Business.Interfaces
{
    public interface IQuestionRepository
    {
        Question GetRandom(IEnumerable<int> excludedQuestionsIds);
    }
}
