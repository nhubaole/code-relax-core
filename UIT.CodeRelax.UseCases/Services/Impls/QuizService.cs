using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UIT.CodeRelax.Core.Entities;
using UIT.CodeRelax.UseCases.DTOs.Responses;
using UIT.CodeRelax.UseCases.Repositories;
using UIT.CodeRelax.UseCases.Services.Interfaces;

namespace UIT.CodeRelax.UseCases.Services.Impls
{
    public class QuizService : IQuizService
    {
        private readonly IQuizRepository quizRepository;
        private readonly IConfiguration _config;
        private readonly ILogger<ArticleService> logger;

        public QuizService(IQuizRepository quizRepository, IConfiguration config, ILogger<ArticleService> logger)
        {
            this.quizRepository = quizRepository;
            _config = config;
            this.logger = logger;
        }

        public async Task<APIResponse<Quiz>> AddQuizAsync(Quiz Quiz)
        {
           try
            {
                var response = await quizRepository.AddQuizAsync(Quiz);

                return new APIResponse<Quiz>
                {
                    StatusCode = StatusCodeRes.Success,
                    Data = response,
                };
            }
            catch (Exception ex)
            {
                return new APIResponse<Quiz>
                {
                    Message = ex.Message,
                    StatusCode = StatusCodeRes.InternalError,
                };
            }
        }

        public async Task<APIResponse<Quiz>> DeleteQuizAsync(int id)
        {

            try
            {
                await quizRepository.DeleteQuizAsync(id);

                return new APIResponse<Quiz>
                {
                    StatusCode = StatusCodeRes.Success,
                    Data = null,
                };
            }
            catch (Exception ex)
            {
                return new APIResponse<Quiz>
                {
                    Message = ex.Message,
                    StatusCode = StatusCodeRes.InternalError,
                };
            }
        }

        public async Task<APIResponse<IEnumerable<Quiz>>> GetAllQuizsAsync()
        {

            try
            {
                var response = await quizRepository.GetAllQuizesAsync();

                return new APIResponse<IEnumerable<Quiz>>
                {
                    StatusCode = StatusCodeRes.Success,
                    Data = response,
                };
            }
            catch (Exception ex)
            {
                return new APIResponse<IEnumerable<Quiz>>
                {
                    Message = ex.Message,
                    StatusCode = StatusCodeRes.InternalError,
                };
            }
        }

        public async Task<APIResponse<Quiz>> GetQuizByIdAsync(int id)
        {

            try
            {
                var response = await quizRepository.GetQuizByIdAsync(id);

                return new APIResponse<Quiz>
                {
                    StatusCode = StatusCodeRes.Success,
                    Data = response,
                };
            }
            catch (Exception ex)
            {
                return new APIResponse<Quiz>
                {
                    Message = ex.Message,
                    StatusCode = StatusCodeRes.InternalError,
                };
            }
        }


        public async Task<APIResponse<Quiz>> UpdateQuizAsync(Quiz Quiz)
        {

            try
            {
                var response = await quizRepository.UpdateQuizAsync(Quiz);

                return new APIResponse<Quiz>
                {
                    StatusCode = StatusCodeRes.Success,
                    Data = response,
                };
            }
            catch (Exception ex)
            {
                return new APIResponse<Quiz>
                {
                    Message = ex.Message,
                    StatusCode = StatusCodeRes.InternalError,
                };
            }
        }
    }
}
