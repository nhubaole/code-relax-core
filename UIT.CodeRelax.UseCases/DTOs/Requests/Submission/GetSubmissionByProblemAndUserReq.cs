using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UIT.CodeRelax.UseCases.DTOs.Requests.Discussion;

namespace UIT.CodeRelax.UseCases.DTOs.Requests.Submission
{
    public class GetSubmissionByProblemAndUserReq
    {
        public int ProblemID { get; set; }
        public int UserID { get; set; }
    }

    public class GetSubmissionByProblemAndUserReqValidator : AbstractValidator<GetSubmissionByProblemAndUserReq>
    {
        public GetSubmissionByProblemAndUserReqValidator()
        {
            RuleFor(x => x.UserID).NotNull().GreaterThan(0);
            RuleFor(x => x.ProblemID).NotNull().GreaterThan(0);
        }
    }
}
