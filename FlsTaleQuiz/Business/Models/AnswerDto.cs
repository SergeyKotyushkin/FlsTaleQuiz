namespace FlsTaleQuiz.Business.Models
{
    public class AnswerDto
    {
        public long Id { get; set; }

        public string Text { get; set; }

        public bool IsRight { get; set; }
    }
}