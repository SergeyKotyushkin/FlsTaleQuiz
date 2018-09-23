using System.Collections.Generic;
using FlsTaleQuiz.Business.Models;

namespace FlsTaleQuiz.Business.Interfaces
{
    public interface IAnswerRepository
    {
        IEnumerable<Answer> GetByIds(IEnumerable<int> answersIds);
    }
}
