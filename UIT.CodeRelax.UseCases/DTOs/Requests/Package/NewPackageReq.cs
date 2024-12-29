using FluentValidation;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UIT.CodeRelax.UseCases.DTOs.Requests.Package
{
    public class NewPackageReq
    {
        public string Type { get; set; }
        public string Content { get; set; }   
    }

    public class NewPackageReqValidator : AbstractValidator<NewPackageReq>
    {
        public NewPackageReqValidator() {
            RuleFor(x => x.Type).NotEmpty(); 
            RuleFor(x => x.Content).NotEmpty();
        }
    }
}
