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
        public int NumOfAcceptance { get; set; } = 0;
        [Column("num_of_submission")]
        public int NumOfSubmission { get; set; } = 0;
        [Column("created_at")]
        public DateTime CreatedAt { get; set; }  = DateTime.Now;

        // one to many testcase
        public ICollection<Testcase> Testcases { get; set; } = new List<Testcase>();

        // one to many submission
        public ICollection<Submission> Submissions { get; set; } = new List<Submission>();

        // one to many problem_tag
        public ICollection<ProblemTag> ProblemTags { get; set; } = new List<ProblemTag>();

        //relation one to many with problem package 
        public ICollection<ProblemPackage> ProblemPackages { get; set; } = new List<ProblemPackage>();

        //relation one to many with discussion 
        public ICollection<Discussion> Discussions { get; set; } = new List<Discussion>();

        //relation one to many with rating 
        public ICollection<Rating> ratings { get; set; } = new List<Rating>();


    }
}
