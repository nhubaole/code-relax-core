﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;
using UIT.CodeRelax.UseCases.DTOs.Requests.Authentication;

namespace UIT.CodeRelax.UseCases.DTOs.Requests.Article
{
    public class ArticleInfoReq
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;

        public int LikeCount { get; set; } = 0;
        public int UserId { get; set; }
    }
    public class ArticleInforvalidator : AbstractValidator<ArticleInfoReq>
    {
        public ArticleInforvalidator()
        {
            RuleFor(x => x.Title).NotEmpty();
            RuleFor(x => x.Content).NotEmpty();
            RuleFor(x => x.UserId).NotEmpty();

        }
    }

}
