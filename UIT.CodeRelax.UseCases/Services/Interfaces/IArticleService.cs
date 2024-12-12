using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UIT.CodeRelax.Core.Entities;
using UIT.CodeRelax.UseCases.DTOs.Responses;

namespace UIT.CodeRelax.UseCases.Services.Interfaces
{
    public interface IArticleService
    {
        Task<APIResponse<IEnumerable<Article>>> GetAllArticlesAsync();
        Task<APIResponse<IEnumerable<Article>>> GetArticleByUserId(int userId);
        Task<APIResponse<Article>> GetArticleByIdAsync(int id);
        Task<APIResponse<Article>> AddArticleAsync(Article article);
        Task<APIResponse<Article>>  UpdateArticleAsync(Article article);
        Task<APIResponse<Article>>DeleteArticleAsync(int id);
    }
}
