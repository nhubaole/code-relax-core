using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UIT.CodeRelax.Core.Entities
{
    [Table("discussions")]
    public class Discussion
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("id")]
        public int Id { get; set; }

        [Column("content")]
        public string Content { get; set; }

        [Column("type")]
        public string Type { get; set; }

        [Column("created_at")]
        public DateTime CreatedAt { get; set; } = DateTime.Now;


        [ForeignKey("User")]
        [Column("user_id")]
        public int User_id { get; set; }    
        public User User { get; set; }

        [ForeignKey("Problem")]
        [Column("problem_id")]
        public int Problem_id { get; set; } 
        public Problem Problem { get; set; }
    }
}
