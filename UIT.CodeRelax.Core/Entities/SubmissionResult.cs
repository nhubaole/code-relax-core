using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UIT.CodeRelax.Core.Entities
{
    [Table("submission_results")]
    public class SubmissionResult
    {
        [ForeignKey("Submission")]
        [Column("submission_id")]
        public int SubmissionId { get; set; }
        public Submission Submission { get; set; }  

        [ForeignKey("Testcase")]
        [Column("passed_testcase_id")]
        public int PassedTestcaseId { get; set;}
        public Testcase Testcase { get; set; }  

    }
}
