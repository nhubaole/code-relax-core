using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UIT.CodeRelax.Core.Entities;
using UIT.CodeRelax.Infrastructure.DataAccess;
using UIT.CodeRelax.UseCases.DTOs.Responses.Problem;
using UIT.CodeRelax.UseCases.Repositories;

namespace UIT.CodeRelax.Infrastructure.Repositories
{
    public class ProblemRepository : IProblemRepository
    {
        private readonly AppDbContext _dbContext;
        private readonly IMapper _mapper;
        public ProblemRepository(AppDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }
        public async Task<IEnumerable<GetProblemRes>> GetAllAsync()
        {
            try
            {
                var problems = await _dbContext.Problems.ToArrayAsync();

                return _mapper.Map<IEnumerable<GetProblemRes>>(problems);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public async Task<GetProblemRes> GetByIDAsync(int id)
        {
            try
            {
                var problem = await _dbContext.Problems.FirstOrDefaultAsync(x => x.Id == id);
                return _mapper.Map<GetProblemRes>(problem);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


    }
}
