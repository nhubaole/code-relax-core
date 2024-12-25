using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UIT.CodeRelax.Core.Entities
{
    [Table("users")]
    public class User
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("id")]
        public int Id { get; set; }
        [Required]
        [Column("email")]
        public string Email { get; set; } = string.Empty;
        [Required]
        [Column("password")]
        public string Password { get; set; } = string.Empty;
        [Column("display_name")]
        public string DisplayName { get; set; } = string.Empty;
        [Column("role")]
        public int Role { get; set; }
        [Column("created_at")]
        public DateTime CreatedAt { get; set; } = DateTime.Now;

        //relation one to many with submissions 
        public ICollection<Submission> Submissions { get; set; } = new List<Submission>();

        //relation one to many with discussion 
        public ICollection<Discussion> Discussions { get; set; } = new List<Discussion>();

        //relation one to many with rating 
        public ICollection<Rating> ratings { get; set; } = new List<Rating>();

    }
}


