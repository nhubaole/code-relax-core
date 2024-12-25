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
    public class QuizRepository : IQuizRepository
    {
        private readonly AppDbContext _dbContext;
        private readonly IMapper _mapper;

        public QuizRepository(AppDbContext _dbContext, IMapper _mapper)
        {
            this._dbContext = _dbContext;
            this._mapper = _mapper;
        }
        public async Task<Quiz> AddQuizAsync(Quiz Quiz)
        {
            await _dbContext.Quizzes.AddAsync(Quiz);
            await _dbContext.SaveChangesAsync();
            return Quiz;
        }

        public async Task DeleteQuizAsync(int id)
        {
            var quiz = await GetQuizByIdAsync(id);
            if (quiz != null)
            {
                _dbContext.Quizzes.Remove(quiz);
                await _dbContext.SaveChangesAsync();
            }

            else { throw new Exception($"Quiz with id {id} does not exist"); };

        }

        public async Task<IEnumerable<Quiz>> GetAllQuizesAsync()
        {
            var res = await _dbContext.Quizzes.ToListAsync();
            return res;
        }

        public async Task<Quiz> GetQuizByIdAsync(int id)
        {
            var res = await _dbContext.Quizzes.FirstOrDefaultAsync(quiz => quiz.Id == id);
            if (res == null)
            {
                return null;
            }

            return res;
        }

        public async Task<Quiz> UpdateQuizAsync(Quiz Quiz)
        {
            var existingQuiz = await GetQuizByIdAsync(Quiz.Id);
            if (existingQuiz != null)
            {
                existingQuiz.QuestionText = Quiz.QuestionText;
                existingQuiz.OptionA = Quiz.OptionA;
                existingQuiz.OptionB = Quiz.OptionB;
                existingQuiz.OptionC = Quiz.OptionC;
                existingQuiz.OptionD = Quiz.OptionD;
                existingQuiz.CorrectOption = Quiz.CorrectOption;
                existingQuiz.Explanation = Quiz.Explanation;
                existingQuiz.ArticleId = Quiz.ArticleId;
                //_dbContext.Quizzes.Update(existingQuiz);
                await _dbContext.SaveChangesAsync();
                return existingQuiz;
            }
            else { throw new Exception($"Quiz with id {Quiz.Id} does not exist"); };
        }
    }
}
