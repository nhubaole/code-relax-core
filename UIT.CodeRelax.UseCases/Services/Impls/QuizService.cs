using AutoMapper;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UIT.CodeRelax.Core.Entities;
using UIT.CodeRelax.UseCases.DTOs.Requests.Quiz;
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
        private readonly IMapper _mapper;


        public QuizService(IQuizRepository quizRepository, IConfiguration config, ILogger<ArticleService> logger, IMapper mapper)
        {
            this.quizRepository = quizRepository;
            _config = config;
            this.logger = logger;
            _mapper = mapper;
        }

        public async Task<APIResponse<Quiz>> AddQuizAsync(CreateQuizReq Quiz)
        {
            try
            {
                var model = _mapper.Map<Quiz>(Quiz);
                var response = await quizRepository.AddQuizAsync(model);

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


        public async Task<APIResponse<Quiz>> UpdateQuizAsync(int id, CreateQuizReq Quiz)
        {

            try
            {
                var model = MapToQuiz(Quiz);
                var response = await quizRepository.UpdateQuizAsync(model);

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

        public CreateQuizReq MapToReq(Quiz quiz)
        {
            return new CreateQuizReq
            {
                QuestionText = quiz.QuestionText,
                OptionA = quiz.OptionA,
                OptionB = quiz.OptionB,
                OptionC = quiz.OptionC,
                OptionD = quiz.OptionD,
                CorrectOption = quiz.CorrectOption,
                Explanation = quiz.Explanation,
            };
        }

        public Quiz MapToQuiz(CreateQuizReq quizReq)
        {
            return new Quiz
            {
                Id = 1,
                QuestionText = quizReq.QuestionText,
                OptionA = quizReq.OptionA,
                OptionB = quizReq.OptionB,
                OptionC = quizReq.OptionC,
                OptionD = quizReq.OptionD,
                CorrectOption = quizReq.CorrectOption,
                Explanation = quizReq.Explanation,

            };
        }
    }
}
