using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UIT.CodeRelax.Core.Entities
{
    [Table("ratings")]
    public class Rating
    {
        [ForeignKey("User")]
        [Column("user_id")]
        public int UserID { get; set; }
        public User User { get; set; }

        [ForeignKey("Problem")]
        [Column("problem_id")]

        public int ProblemID { get; set; }
        public Problem Problem { get; set; }
        [Column("num_of_star")]
        public int NumberOfStar { get; set; }
        [Column("created_at")]
        public DateTime CreatedAt { get; set; }
        [Column("updated_at")]
        public DateTime UpdatedAt { get; set; }
    }
}
