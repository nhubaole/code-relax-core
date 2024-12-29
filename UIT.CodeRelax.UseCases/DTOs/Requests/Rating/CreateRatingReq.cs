using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UIT.CodeRelax.UseCases.DTOs.Requests.Rating
{
    public class CreateRatingReq
    {
        public int NumberOfStar { get; set; }
        public int UserID { get; set; }
        public int ProblemID { get; set; }

    }

    public class CreateRatingReqValidator : AbstractValidator<CreateRatingReq>
    {
        public CreateRatingReqValidator()
        {
            RuleFor(x => x.NumberOfStar).NotNull().GreaterThanOrEqualTo(0);
            RuleFor(x => x.UserID).NotNull().GreaterThan(0);
            RuleFor(x => x.ProblemID).NotNull().GreaterThan(0);
        }
    }
}
