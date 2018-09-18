using System.Collections.Generic;
using FlsTaleQuiz.Business.Models;

namespace FlsTaleQuiz.Business.Interfaces
{
    public interface IAnswerDtoRepository
    {
        IEnumerable<AnswerDto> GetByIds(IEnumerable<long> answersIds);
    }
}
