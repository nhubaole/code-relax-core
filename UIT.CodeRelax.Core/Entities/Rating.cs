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
        public int User_Id { get; set; }    
        public User User { get; set; }

        [ForeignKey("Problem")]
        [Column("problem_id")]

        public int Problem_Id { get; set; }
        public Problem Problem { get; set; }

        public int NumberOfStar { get; set; }
    }
}
