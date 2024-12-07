using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UIT.CodeRelax.UseCases.DTOs.Responses.Problem;
using UIT.CodeRelax.UseCases.DTOs.Responses.User;

namespace UIT.CodeRelax.UseCases.DTOs.Responses.Submission
{
    public class GetSubmissionRes
    {
        public int ID { get; set; }
        public string Code { get; set; }
        public string Language { get; set; }
        public GetProblemRes Problem { get; set; }
        public int UserID { get; set; }
        public int Status { get; set; }
        public string Result { get; set; }
        public string Note { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
