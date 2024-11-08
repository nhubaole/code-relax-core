using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UIT.CodeRelax.Core.Entities
{
    [Table("problem_tags")]
    public class ProblemTag
    {
        [ForeignKey("Problem")]
        [Column("problem_id")]
        public int ProblemId { get; set; }
        public Problem Problem { get; set; }

        [ForeignKey("Tag")]
        [Column("tag_id")]
        public int TagId { get; set; }
        public Tag Tag { get; set; }

    }
}
