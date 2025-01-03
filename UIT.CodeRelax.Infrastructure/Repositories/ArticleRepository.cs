﻿using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UIT.CodeRelax.Core.Entities;
using UIT.CodeRelax.Infrastructure.DataAccess;
using UIT.CodeRelax.UseCases.DTOs.Requests.Article;
using UIT.CodeRelax.UseCases.Helper;
using UIT.CodeRelax.UseCases.Repositories;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

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
                                  .ToListAsync();
        }
        public async Task<Article> GetArticleByIdAsync(int id)
        {
            var article = await _dbContext.Articles
                                  .FirstOrDefaultAsync(a => a.Id == id);

            if (article == null)
            {
                return null; 
            }

            return article;
        }
        public async Task<int> CreateAsync(Article article)
        {
            try
            {
                await _dbContext.Articles.AddAsync(article);
                await _dbContext.SaveChangesAsync();
                return article.Id;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public async Task<Article> UpdateArticleAsync(Article article)
        {
            _dbContext.Articles.Update(article);
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
            else
            {
                throw new Exception($"Article with id {id} does not exist");
            }
        }
        public async Task<Article> GetArticleByIdWithQuizzesAsync(int id)
        {
            var queery = _dbContext.Articles
        .Include(a => a.quizzes)
        .Where(a => a.Id == id);
            Console.WriteLine(queery.ToQueryString());
            var article = await queery.FirstOrDefaultAsync();

            if (article == null)
            {
                throw new Exception($"Article with ID {id} not found.");
            }
            return article;
        }
    }
}
