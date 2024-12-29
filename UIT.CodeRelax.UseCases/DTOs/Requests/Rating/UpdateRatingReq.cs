using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UIT.CodeRelax.UseCases.DTOs.Requests.Rating
{
    public class UpdateRatingReq
    {
        public int NumberOfStar { get; set; }
    }
    public class UpdateRatingReqValidator : AbstractValidator<UpdateRatingReq>
    {
        public UpdateRatingReqValidator()
        {
            RuleFor(x => x.NumberOfStar).NotNull().GreaterThanOrEqualTo(0);

        }
    }
}
