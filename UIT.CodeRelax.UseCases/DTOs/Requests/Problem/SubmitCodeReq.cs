using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UIT.CodeRelax.UseCases.DTOs.Requests.Problem
{
    public class SubmitCodeReq
    {
        public int ProblemId { get; set; }
        public string SourceCode { get; set; }
        public string Language { get; set; }
    }
}
