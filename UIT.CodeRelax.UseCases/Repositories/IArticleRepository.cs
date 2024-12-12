using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UIT.CodeRelax.Core.Entities;

namespace UIT.CodeRelax.UseCases.Repositories
{
    public interface IArticleRepository
    {
        Task<IEnumerable<Article>> GetAllArticlesAsync();
        Task<Article> GetArticleByIdAsync(int id);
        Task<Article> AddArticleAsync(Article article);
        Task<Article> UpdateArticleAsync(Article article);
        Task DeleteArticleAsync(int id);
        Task<IEnumerable<Article>> GetArticleByUserIdAsync(int userId);
    }
}
