using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace UIT.CodeRelax.UseCases.DTOs.Requests.Submission
{
    public class CreateSubmissionReq
    {
        public int ProblemID { get; set; }
        public int UserID { get; set; }
        public string Code { get; set; }
        public int Language { get; set; }
        public int Status { get; set; }
    }
}
