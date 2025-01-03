﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using UIT.CodeRelax.Core.Entities;
using UIT.CodeRelax.UseCases.DTOs.Responses.Quiz;

namespace UIT.CodeRelax.UseCases.DTOs.Responses.Articles
{
    public class ArticleInforRes
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Summary { get; set; }
        public string Cover { get; set; }
        public List<string> SubTitle { get; set; }
        public List<string> Content { get; set; } 
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public int UserId { get; set; }
        public ICollection<QuizInforRes> Quizzes { get; set; } = new List<QuizInforRes>();
    }
}
