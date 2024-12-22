using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UIT.CodeRelax.UseCases.DTOs.Requests.Quiz
{
    public class QuizInforReq
    {
        public int Id { get; set; }
        public string QuestionText { get; set; }
        public string OptionA { get; set; }
        public string OptionB { get; set; }
        public string OptionC { get; set; }
        public string OptionD { get; set; }
        public string CorrectOption { get; set; }
        public string Explanation { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public int Article_id { get; set; }
    }
}
