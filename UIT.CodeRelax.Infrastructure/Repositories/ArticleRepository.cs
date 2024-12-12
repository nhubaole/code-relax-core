using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UIT.CodeRelax.Core.Entities;
using UIT.CodeRelax.Infrastructure.DataAccess;
using UIT.CodeRelax.UseCases.Repositories;

namespace UIT.CodeRelax.Infrastructure.Repositories
{
    public class ArticleRepository : IArticleRepository
    {
        private readonly AppDbContext _dbContext;
        private readonly IMapper _mapper;
        public ArticleRepository(AppDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }
        public async Task<IEnumerable<Article>> GetAllArticlesAsync()
        {
            return await _dbContext.Articles
                                  .Include(a => a.User)
                                  .ToListAsync();
        }
        public async Task<Article> GetArticleByIdAsync(int id)
        {
            var article = await _dbContext.Articles
                                  .Include(a => a.User)
                                  .FirstOrDefaultAsync(a => a.Id == id);

            if (article == null)
            {
                return null; 
            }

            return article;
        }
        public async Task<Article> AddArticleAsync(Article article)
        {
            await _dbContext.Articles.AddAsync(article);  
            await _dbContext.SaveChangesAsync();
            return article;
        }
        public async Task<Article> UpdateArticleAsync(Article article)
        {
            await _dbContext.Articles.AddAsync(article);
            await _dbContext.SaveChangesAsync();
            return article;
        }
        public async Task DeleteArticleAsync(int id)
        {
            var article = await _dbContext.Articles.FindAsync(id); 
            if (article != null)
            {
                _dbContext.Articles.Remove(article);
                await _dbContext.SaveChangesAsync();
            }
        }

        public async Task<IEnumerable<Article>> GetArticleByUserIdAsync(int userId)
        {
            return await _dbContext.Articles.Where(a => a.UserId == userId)
                                 .ToListAsync();
        }
    }
}
