using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;
using UIT.CodeRelax.UseCases.DTOs.Requests.Authentication;
using UIT.CodeRelax.UseCases.DTOs.Requests.Quiz;
using Microsoft.AspNetCore.Http;

namespace UIT.CodeRelax.UseCases.DTOs.Requests.Article
{
    public class CreateArticleReq
    {
        public string Title { get; set; }
        public string Summary { get; set; }
        public List<string> SubTitle { get; set; }
        public IFormFile Cover { get; set; }
        public List<string> Content { get; set; }
    }
    public class ArticleInforvalidator : AbstractValidator<CreateArticleReq>
    {
        public ArticleInforvalidator()
        {
            RuleFor(x => x.Title).NotEmpty();
            RuleFor(x => x.Content).NotEmpty();
        }
    }

}
