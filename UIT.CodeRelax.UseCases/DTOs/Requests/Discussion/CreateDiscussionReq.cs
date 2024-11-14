using FluentValidation;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UIT.CodeRelax.UseCases.DTOs.Requests.Authentication;

namespace UIT.CodeRelax.UseCases.DTOs.Requests.Discussion
{
    public class CreateDiscussionReq
    {
        public string Content { get; set; }
        public string Type { get; set; }
        public int UserID { get; set; }
        public int ProblemID { get; set; }

    }

    public class CreateDiscussionReqValidator : AbstractValidator<CreateDiscussionReq>
    {
        public CreateDiscussionReqValidator()
        {
            RuleFor(x => x.Content).NotEmpty().NotNull();
            RuleFor(x => x.Type).NotEmpty().NotNull();
            RuleFor(x => x.UserID).NotNull().GreaterThan(0);
            RuleFor(x => x.ProblemID).NotNull().GreaterThan(0);
        }
    }
}
