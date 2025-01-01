
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json;

namespace UIT.CodeRelax.Core.Entities
{
    [Table("articles")]
    public class Article
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("id")]
        public int Id { get; set; }

        [Column("title")]
        public string Title { get; set; }

        [Column("summary")]
        public string Summary { get; set; }

        [Column("subtitle")]
        public List<string> SubTitle { get; set; }

        [Column("cover")]
        public string Cover { get; set; }

        [Column("content")]
        public List<string> Content { get; set; }

        [Column("created_at")]
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        [Column("updated_at")]
        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;

        public ICollection<Quiz> quizzes { get; set; } = new List<Quiz>();

        public int GetDaysSinceUpdated()
        {
            return (DateTime.Now - UpdatedAt).Days;
        }
    }
}