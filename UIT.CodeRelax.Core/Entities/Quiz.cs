using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UIT.CodeRelax.Core.Entities
{
    [Table("quizzes")]
    public class Quiz
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("id")]
        public int Id { get; set; }

        [Column("question_text")]
        public string QuestionText { get; set; }

        [Column("option_a")]
        public string OptionA { get; set; }

        [Column("option_b")]
        public string OptionB { get; set; }

        [Column("option_c")]
        public string OptionC { get; set; }

        [Column("option_d")]
        public string OptionD { get; set; }

        [Column("correct_option")]
        public string CorrectOption { get; set; }

        [Column("explanation")]
        public string Explanation { get; set; }

        [Column("created_at")]
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        [ForeignKey("Article")]
        [Column("article_id")]
        public int Article_id { get; set; }
        public Article? article { get; set; }
    }
}
