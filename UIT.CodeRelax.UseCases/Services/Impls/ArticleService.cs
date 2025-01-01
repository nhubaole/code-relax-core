using AutoMapper;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Transactions;
using UIT.CodeRelax.Core.Entities;
using UIT.CodeRelax.UseCases.DTOs.Requests.Article;
using UIT.CodeRelax.UseCases.DTOs.Responses;
using UIT.CodeRelax.UseCases.DTOs.Responses.Articles;
using UIT.CodeRelax.UseCases.DTOs.Responses.Quiz;
using UIT.CodeRelax.UseCases.Helper;
using UIT.CodeRelax.UseCases.Repositories;
using UIT.CodeRelax.UseCases.Services.Interfaces;

namespace UIT.CodeRelax.UseCases.Services.Impls
{
    public class ArticleService : IArticleService
    {
        private readonly IArticleRepository _articleRepository;
        private readonly IStorageService _storageService;
        private readonly IQuizRepository _quizRepository;
        private readonly IConfiguration _config;
        private readonly ILogger<ArticleService> logger;
        private readonly IMapper _mapper;


        public ArticleService(IArticleRepository articleRepository, IConfiguration _config, ILogger<ArticleService> logger, IMapper mapper, IStorageService storageService, IQuizRepository quizRepository)
        {
            this._articleRepository = articleRepository;
            this._config = _config;
            this.logger = logger;
            _mapper = mapper;
            _storageService = storageService;
            _quizRepository = quizRepository;
        }

        public async Task<APIResponse<int>> CreateAsync(CreateArticleReq articleInfor)
        {
            try
            {
                using (var scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
                {
                    var coverUrl = await _storageService.Upload(articleInfor.Cover, "covers-image", Generator.GetNextSerial());
                    var article = new Article
                    {
                        Title = articleInfor.Title,
                        Summary = articleInfor.Summary,
                        Content = articleInfor.Content,
                        SubTitle = articleInfor.SubTitle,
                        Cover = coverUrl.Data != null ? coverUrl.Data : "",
                    };

                    var response = await _articleRepository.CreateAsync(article);

                    // Nếu tất cả đều thành công, commit transaction
                    scope.Complete();

                    if (response != 0)
                    {
                        return new APIResponse<int>
                        {
                            StatusCode = StatusCodeRes.Success,
                            Data = response
                        };
                    }

                    return new APIResponse<int>
                    {
                        StatusCode = StatusCodeRes.InternalError,
                        Message = "method's not success, please check the input"
                    };
                }
            }
            catch (Exception ex)
            {
                return new APIResponse<int>
                {
                    StatusCode = StatusCodeRes.InternalError,
                    Message = ex.Message
                };
            }
        }

        public async Task<APIResponse<ArticleInforRes>> DeleteArticleAsync(int id)
        {
            try
            {
                await _articleRepository.DeleteArticleAsync(id);
                return new APIResponse<ArticleInforRes>
                {
                    StatusCode = StatusCodeRes.Success,
                    Data = null
                };
            }
            catch (Exception ex)
            {
                return new APIResponse<ArticleInforRes>
                {
                    StatusCode = StatusCodeRes.InternalError,
                    Message=ex.Message,
                };
            }
        }

        public async Task<APIResponse<IEnumerable<ArticleInforRes>>> GetAllArticlesAsync()
        {
            try
            {
                var response = await _articleRepository.GetAllArticlesAsync();
                var result = response.Select(artile => MapToArticleResponse(artile));

                return new APIResponse<IEnumerable<ArticleInforRes>>()
                {
                    StatusCode = StatusCodeRes.Success,
                    Data = result,
                };
            }
            catch (Exception ex)
            {
                return new APIResponse<IEnumerable<ArticleInforRes>>()
                {
                    StatusCode = StatusCodeRes.InternalError,
                    Message = ex.Message,
                };
            }
        }

        public async Task<APIResponse<ArticleInforRes>> GetArticleByIdAsync(int id)
        {
            try
            {
                var response = await _articleRepository.GetArticleByIdAsync(id);

                return new APIResponse<ArticleInforRes>()
                {
                    StatusCode = StatusCodeRes.Success,
                    Data = MapToArticleResponse(response)
                };
            }
            catch (Exception ex)
            {
                return new APIResponse<ArticleInforRes>()
                {
                    StatusCode = StatusCodeRes.InternalError,
                    Message = ex.Message,
                };
            }
        }
        public async Task<APIResponse<ArticleInforRes>> UpdateArticleAsync(CreateArticleReq articleInfor)
        {
            try
            {
                Article article = new Article();
                //article = MapToArticle(articleInfor);


                var response = await _articleRepository.UpdateArticleAsync(article);
                return new APIResponse<ArticleInforRes>()
                {
                    StatusCode = StatusCodeRes.Success,
                    Data = MapToArticleResponse(response),
                };
            }
            catch (Exception ex)
            {
                return new APIResponse<ArticleInforRes>()
                {
                    StatusCode = StatusCodeRes.InternalError,
                    Message = ex.Message,
                };
            }
        }

        public ArticleInforRes MapToArticleResponse(Article article)
        {
            ArticleInforRes res = new ArticleInforRes
            {
                Id = article.Id,
                Title = article.Title,
                Summary = article.Summary,
                Cover = article.Cover,
                SubTitle = article.SubTitle != null
                        ? article.SubTitle
                        : new List<string>(),
                Content = article.Content != null
                        ? article.Content
                        : new List<string>(),
                CreatedAt = article.CreatedAt,
                UpdatedAt = article.UpdatedAt,
            };
            foreach (Quiz quiz in article.quizzes)
            {
                QuizInforRes quizInfor = new QuizInforRes
                {
                    Id = quiz.Id,
                    QuestionText = quiz.QuestionText,
                    OptionA = quiz.OptionA,
                    OptionB = quiz.OptionB,
                    OptionC = quiz.OptionC,
                    OptionD = quiz.OptionD,
                    CorrectOption = quiz.CorrectOption,
                    Explanation = quiz.Explanation,
                    CreatedAt = quiz.CreatedAt,
                    ArticleId = quiz.ArticleId
                };
                res.Quizzes.Add(quizInfor);
            }
            return res;
        }

        //public Article MapToArticle(CreateArticleReq dto)
        //{
        //    return new Article
        //    {
        //        Id = dto.Id,
        //        Title = dto.Title,
        //        Summary = dto.Summary,
        //        Cover = dto.Cover,
        //        SubTitle = dto.SubTitle != null
        //            ? dto.SubTitle
        //            : null, 
        //        Content = dto.Content != null
        //            ? dto.Content
        //            : null, 
        //        CreatedAt = dto.CreatedAt,  
        //        UpdatedAt = dto.UpdatedAt,
        //    };
        //}

        public async Task<APIResponse<ArticleInforRes>> GetArticleAndQuizzesByIdAsync(int id)
        {
            try
            {
                var response = await _articleRepository.GetArticleByIdWithQuizzesAsync(id);
                if(response != null)
                {
                    var res = MapToArticleResponse(response);
                    return new APIResponse<ArticleInforRes>()
                    {
                        StatusCode = StatusCodeRes.Success,
                        Data = res,
                    };
                }
                return new APIResponse<ArticleInforRes>()
                {
                    StatusCode = StatusCodeRes.InternalError,
                    Message ="Can not find the article",
                };
            }
            catch (Exception ex)
            {
                return new APIResponse<ArticleInforRes>()
                {
                    StatusCode = StatusCodeRes.InternalError,
                    Message = ex.Message,   
                };
            }
        }
    }
}
