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

        private string _password; // original

        public string Password
        {
            get => _password;
            set => _password = ConvertToBase64(value); 
        }

        // Encode password to base 64
        private string ConvertToBase64(string password)
        {
            if (string.IsNullOrEmpty(password))
            {
                return password;
            }

            byte[] passwordBytes = Encoding.UTF8.GetBytes(password);
            return Convert.ToBase64String(passwordBytes);
        }
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
