using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace UIT.CodeRelax.UseCases.DTOs.Requests.Authentication
{
    public class LoginReq
    {
        public string Email { get; set; }
        public string Password { get; set; }
    }

    public class LoginReqValidator : AbstractValidator<LoginReq>
    {
        public LoginReqValidator()
        {
            RuleFor(x => x.Email).EmailAddress().WithMessage("Invalid Format");
            RuleFor(x => x.Password).NotEmpty();
        }
    }
}
