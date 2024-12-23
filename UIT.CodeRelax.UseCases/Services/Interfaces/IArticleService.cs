using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UIT.CodeRelax.Core.Entities;
using UIT.CodeRelax.UseCases.DTOs.Requests.Article;
using UIT.CodeRelax.UseCases.DTOs.Requests.User;
using UIT.CodeRelax.UseCases.DTOs.Responses;
using UIT.CodeRelax.UseCases.DTOs.Responses.Articles;

namespace UIT.CodeRelax.UseCases.Services.Interfaces
{
    public interface IArticleService
    {
        Task<APIResponse<IEnumerable<ArticleInforRes>>> GetAllArticlesAsync();
        Task<APIResponse<IEnumerable<ArticleInforRes>>> GetArticleByUserId(int userId);
        Task<APIResponse<ArticleInforRes>> GetArticleByIdAsync(int id);
        Task<APIResponse<ArticleInforRes>> AddArticleAsync(ArticleInfoReq article);
        Task<APIResponse<ArticleInforRes>> UpdateArticleAsync(ArticleInfoReq article);
        Task<APIResponse<ArticleInforRes>>DeleteArticleAsync(int id);

        Task<APIResponse<ArticleInforRes>> GetArticleAndQuizzesByIdAsync(int id);
    }
}
