using FluentValidation;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UIT.CodeRelax.UseCases.DTOs.Requests.Rating;

namespace UIT.CodeRelax.UseCases.DTOs.Requests.Problem
{
    public class UpdateProblemReq
    {
        public string Title { get; set; }
        public string Explaination { get; set; }

        public int Difficulty { get; set; }

        public string? FunctionName { get; set; }

        public string? ReturnType { get; set; }
    }
    public class UpdateProblemReqValidator : AbstractValidator<UpdateProblemReq>
    {
        public UpdateProblemReqValidator()
        {
            RuleFor(x => x.Title).NotNull().NotEmpty();
            RuleFor(x => x.Explaination).NotNull().NotEmpty();
            RuleFor(x => x.Difficulty).NotNull().GreaterThan(0).LessThan(4);
        }
    }
}
