using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UIT.CodeRelax.Core.Entities;
using UIT.CodeRelax.UseCases.DTOs.Requests.Article;
using UIT.CodeRelax.UseCases.DTOs.Responses;
using UIT.CodeRelax.UseCases.Repositories;
using UIT.CodeRelax.UseCases.Services.Interfaces;

namespace UIT.CodeRelax.UseCases.Services.Impls
{
    public class ArticleService : IArticleService
    {
        private readonly IArticleRepository _articleRepository;
        private readonly IConfiguration _config;
        private readonly ILogger<ArticleService> logger;
        
        public ArticleService(IArticleRepository articleRepository, IConfiguration _config, ILogger<ArticleService> logger)
        {
            this._articleRepository = articleRepository;
            this._config = _config;
            this.logger = logger;

        }

        public async Task<APIResponse<Article>> AddArticleAsync(ArticleInfoReq articleInfor)
        {
           try
            {
                Article article = new Article();
                article.Id = articleInfor.Id;
                article.Title = articleInfor.Title;
                article.Content = articleInfor.Content;
                article.UpdatedAt = DateTime.UtcNow;
                article.UserId = articleInfor.UserId;
                
                var response = await _articleRepository.AddArticleAsync(article);
                if(response != null)
                {
                    return new APIResponse<Article> {
                        StatusCode = StatusCodeRes.Success,
                        Data = response
                    };
                }

                return new APIResponse<Article>
                {
                    StatusCode = StatusCodeRes.InternalError,
                    Message = "method's not success, please check the input"
                };
            }
            catch (Exception ex)
            {
                return new APIResponse<Article>
                {
                    StatusCode = StatusCodeRes.InternalError,
                    Message = ex.Message    
                };
            }
        }

        public async Task<APIResponse<Article>> DeleteArticleAsync(int id)
        {
            try
            {
                await _articleRepository.DeleteArticleAsync(id);
                return new APIResponse<Article>
                {
                    StatusCode = StatusCodeRes.Success,
                    Data = null
                };
            }
            catch (Exception ex)
            {
                return new APIResponse<Article>
                {
                    StatusCode = StatusCodeRes.InternalError,
                    Message=ex.Message,
                };
            }
        }

        public async Task<APIResponse<IEnumerable<Article>>> GetAllArticlesAsync()
        {
            try
            {
                var response = await _articleRepository.GetAllArticlesAsync();

                return new APIResponse<IEnumerable<Article>>()
                {
                    StatusCode = StatusCodeRes.Success,
                    Data = response
                };
            }
            catch (Exception ex)
            {
                return new APIResponse<IEnumerable<Article>>()
                {
                    StatusCode = StatusCodeRes.InternalError,
                    Message = ex.Message,
                };
            }
        }

        public async Task<APIResponse<Article>> GetArticleByIdAsync(int id)
        {
            try
            {
                var response = await _articleRepository.GetArticleByIdAsync(id);

                return new APIResponse<Article>()
                {
                    StatusCode = StatusCodeRes.Success,
                    Data = response
                };
            }
            catch (Exception ex)
            {
                return new APIResponse<Article>()
                {
                    StatusCode = StatusCodeRes.InternalError,
                    Message = ex.Message,
                };
            }
        }

        public async Task<APIResponse<IEnumerable<Article>>> GetArticleByUserId(int userId)
        {
            try
            {
                var response = await _articleRepository.GetArticleByUserIdAsync(userId);

                return new APIResponse<IEnumerable<Article>>()
                {
                    StatusCode = StatusCodeRes.Success,
                    Data = response
                };
            }
            catch (Exception ex)
            {
                return new APIResponse<IEnumerable<Article>>()
                {
                    StatusCode = StatusCodeRes.InternalError,
                    Message = ex.Message,
                };
            }
        }

        public async Task<APIResponse<Article>> UpdateArticleAsync(ArticleInfoReq articleInfor)
        {
            try
            {
                Article article = new Article();
                article.Id = articleInfor.Id;
                article.Title = articleInfor.Title;
                article.Content = articleInfor.Content;
                article.UpdatedAt = DateTime.UtcNow;
                article.UserId = articleInfor.UserId; 

                var response = await _articleRepository.UpdateArticleAsync(article);
                return new APIResponse<Article>()
                {
                    StatusCode = StatusCodeRes.Success,
                    Data = response,
                };
            }
            catch (Exception ex)
            {
                return new APIResponse<Article>()
                {
                    StatusCode = StatusCodeRes.InternalError,
                    Message = ex.Message,
                };
            }
        }
    }
}
