using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UIT.CodeRelax.Core.Entities;
using UIT.CodeRelax.Infrastructure.DataAccess;
using UIT.CodeRelax.Infrastructure.Interfaces;

namespace UIT.CodeRelax.UseCases.Repositories
{
    public class JudgeRepository : IJudgeRepository
    {
        private readonly AppDbContext _dbContext;
        public JudgeRepository(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public Problem GetAllProblems()
        {
            var problems = _dbContext.Problems.ToList();
            return problems[0];
        }
    }
}
