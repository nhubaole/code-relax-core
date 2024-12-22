using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UIT.CodeRelax.UseCases.DTOs.Requests.Article;
using UIT.CodeRelax.UseCases.DTOs.Responses.Articles;
using UIT.CodeRelax.UseCases.DTOs.Responses;
using UIT.CodeRelax.Core.Entities;

namespace UIT.CodeRelax.UseCases.Services.Interfaces
{
    public interface IQuizService
    {
        Task<APIResponse<IEnumerable<Quiz>>> GetAllQuizsAsync();
        Task<APIResponse<Quiz>> GetQuizByIdAsync(int id);
        Task<APIResponse<Quiz>> AddQuizAsync(Quiz Quiz);
        Task<APIResponse<Quiz>> UpdateQuizAsync(Quiz Quiz);
        Task<APIResponse<Quiz>> DeleteQuizAsync(int id);
    }
}
