using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace UIT.CodeRelax.Core.Entities
{
    [Table("testcases")]
    public class Testcase
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("id")]
        public int Id { get; set; }

        [Required]
        [Column("input", TypeName = "jsonb")]
        public string Input { get; set; } = JsonSerializer.Serialize(new { });

        [Required]
        [Column("output", TypeName = "jsonb")]
        public string Output { get; set; } = JsonSerializer.Serialize(new { });

        [Column("is_example")]
        public bool IsExample { get; set; }
        [Column("created_at")]
        public DateTime CreatedAt { get; set; }

        [ForeignKey("Problem")]
        [Column("problem_id")]
        public int ProblemId { get; set; }
        public Problem Problem { get; set; }
    }
}
