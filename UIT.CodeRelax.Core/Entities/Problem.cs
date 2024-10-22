using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UIT.CodeRelax.Core.Entities
{
    [Table("problems")]
    public class Problem
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("id")]
        public int Id { get; set; }
        [Required]
        [Column("title")]
        public string Title { get; set; } = string.Empty;
        [Column("explaination")]
        public string? Explaination { get; set; }
        [Column("difficulty")]
        public int Difficulty { get; set; }
        [Column("num_of_acceptance")]
        public int NumOfAcceptance { get; set; }
        [Column("num_of_submission")]
        public int NumOfSubmission { get; set; }
        [Column("created_at")]
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        // one to many testcase
        public ICollection<Testcase> Testcases { get; set; } = new List<Testcase>();
    }
}
