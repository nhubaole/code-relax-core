
using UIT.CodeRelax.Core.Entities;

namespace UIT.CodeRelax.UseCases.Repositories
{
    public interface IQuizRepository
    {
        Task<IEnumerable<Quiz>> GetAllQuizesAsync();
        Task<Quiz> GetQuizByIdAsync(int id);
        Task<Quiz> AddQuizAsync(Quiz Quiz);
        Task<Quiz> UpdateQuizAsync(Quiz Quiz);
        Task DeleteQuizAsync(int id);
    }
}
