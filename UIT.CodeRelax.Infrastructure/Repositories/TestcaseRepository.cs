using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UIT.CodeRelax.Core.Entities;
using UIT.CodeRelax.Infrastructure.DataAccess;
using UIT.CodeRelax.UseCases.DTOs.Responses.Testcase;
using UIT.CodeRelax.UseCases.Repositories;

namespace UIT.CodeRelax.Infrastructure.Repositories
{
    public class TestcaseRepository : ITestcaseRepository
    {
        private readonly AppDbContext _dbContext;
        private readonly IMapper _mapper;
        public TestcaseRepository(AppDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }
        public async Task<IEnumerable<TestcaseRes>> GetByProblemIDAsync(int problemID)
        {
            var testcases = await _dbContext.Testcases
           .Where(t => t.ProblemId == problemID).ToArrayAsync();

            if (testcases == null || testcases.Length == 0)
            {
                return Enumerable.Empty<TestcaseRes>();
            }

            return _mapper.Map<IEnumerable<TestcaseRes>>(testcases);
        }
    }
}
