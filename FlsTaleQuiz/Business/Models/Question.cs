﻿using System.Collections.Generic;

namespace FlsTaleQuiz.Business.Models
{
    public class Question
    {
        public int Id { get; set; }

        public string ImageUrl { get; set; }

        public string Text { get; set; }

        public List<Answer> Answers { get; set; }
    }
}