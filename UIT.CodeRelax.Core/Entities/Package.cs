using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UIT.CodeRelax.Core.Entities
{
    [Table("packages")]
    public class Package
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("id")]
        public int Id { get; set; }

        [Column("type")] 
        public string Type { get; set; }

        [Column("content")]
        public string Content { get; set; }

        [Column("created_at")]
        public DateTime CreatedAt { get; set; }

        [Column("updated_at")]
        public DateTime UpdatedAt { get; set; }


        //relation one to many with problem package 
        public ICollection<ProblemPackage> ProblemPackages { get; set; } = new List<ProblemPackage>();
        public int GetDaysSinceUpdated()
        {
            DateTime currentDate = DateTime.Now;
            TimeSpan timeSpan = currentDate - UpdatedAt;
            return timeSpan.Days;
        }

        //Get level 
        public IEnumerable<int> GetDifficulties()
        {
            return ProblemPackages.Select(pp => pp.Problem.Difficulty).Distinct();
        }

    }
}
