using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UIT.CodeRelax.Core.Entities
{
    [Table("problem_packages")]
    public class ProblemPackage
    {
        [ForeignKey("Package")]
        [Column ("package_id")]
        public int Package_Id { get; set; } 
        public Package Package { get; set; } 

        [ForeignKey("Problem")]
        [Column("problem_id")]
        public int Problem_id { get; set; }
        public Problem Problem { get; set; }


    }
}
