using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UIT.CodeRelax.UseCases.DTOs.Requests.Authentication
{
    public class SignUpReq
    {
        public string Email { get; set; } = String.Empty;
        private string _password;
        public string Password { 
            get => _password; 
            set =>_password = ConvertToBase64(value); 
        } 
        public string DisplayName { get; set; } = String.Empty;

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

    public class SignUpReqValidator : AbstractValidator<SignUpReq>
    {
        public SignUpReqValidator()
        {
            RuleFor(x => x.Email).EmailAddress().WithMessage("Email is invalid Format");
            RuleFor(x => x.Password).NotEmpty().WithMessage("Password is invalid");
            RuleFor(x => x.DisplayName).NotEmpty();
        }
    }
}
