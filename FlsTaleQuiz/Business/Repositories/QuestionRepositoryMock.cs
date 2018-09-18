using System.Collections.Generic;
using System.Linq;
using FlsTaleQuiz.Business.Interfaces;
using FlsTaleQuiz.Business.Models;

namespace FlsTaleQuiz.Business.Repositories
{
    public class QuestionRepositoryMock : IQuestionRepository
    {
        private readonly IQuestionDtoRepository _questionDtoRepository;
        private readonly IQuestionAnswerLinkDtoRepository _questionAnswerLinkDtoRepository;
        private readonly IAnswerDtoRepository _answerDtoRepository;

        public QuestionRepositoryMock(
            IQuestionDtoRepository questionDtoRepository, 
            IQuestionAnswerLinkDtoRepository questionAnswerLinkDtoRepository, 
            IAnswerDtoRepository answerDtoRepository)
        {
            _questionDtoRepository = questionDtoRepository;
            _questionAnswerLinkDtoRepository = questionAnswerLinkDtoRepository;
            _answerDtoRepository = answerDtoRepository;
        }

        public Question GetRandom(IEnumerable<long> excludedQuestionsIds)
        {
            var questionDto = _questionDtoRepository.GetRandom(excludedQuestionsIds);
            if (questionDto == null)
            {
                return null;
            }

            var questionAnswerLinks = _questionAnswerLinkDtoRepository.GetAllByQuestionId(questionDto.Id);
            var questionAnswerLinksArray = questionAnswerLinks == null
                ? new QuestionAnswerLinkDto[0]
                : questionAnswerLinks.ToArray();
            if (questionAnswerLinksArray.Length == 0)
            {
                return null;
            }

            var answersDtos = _answerDtoRepository.GetByIds(questionAnswerLinksArray.Select(e => e.AnswerId));
            var answersArrayDtos = answersDtos == null
                ? new AnswerDto[0]
                : answersDtos.ToArray();
            if (answersArrayDtos.Length == 0)
            {
                return null;
            }

            return ConvertDtosToQuestion(questionDto, answersArrayDtos);
        }

        private static Question ConvertDtosToQuestion(QuestionDto questionDto, IEnumerable<AnswerDto> answersDtos)
        {
            return new Question
            {
                Id = questionDto.Id,
                ImageUrl = questionDto.ImageUrl,
                Text = questionDto.Text,
                Answers = new List<Answer>(answersDtos.Select(a => new Answer {Id = a.Id, Text = a.Text}))
            };
        }
    }
}