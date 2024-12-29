using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UIT.CodeRelax.Core.Entities;
using UIT.CodeRelax.Infrastructure.DataAccess;
using UIT.CodeRelax.UseCases.DTOs.Requests.Problem;
using UIT.CodeRelax.UseCases.DTOs.Responses.Problem;
using UIT.CodeRelax.UseCases.Repositories;

namespace UIT.CodeRelax.Infrastructure.Repositories
{
    public class ProblemRepository : IProblemRepository
    {
        private readonly AppDbContext _dbContext;
        private readonly IMapper _mapper;
        private readonly ITagRespository _tagRespository;
        public ProblemRepository(AppDbContext dbContext, IMapper mapper, ITagRespository tagRespository)
        {
            _dbContext = dbContext;
            _mapper = mapper;
            _tagRespository = tagRespository;
        }

        public async Task<Problem> CreateNewProblem(Problem problemReq, List<string> tags)
        {
            try
            {
                Problem problem = new Problem()
                {
                    Title = problemReq.Title,
                    Explaination = problemReq.Explaination,
                    Difficulty = problemReq.Difficulty,
                    CreatedAt = DateTime.UtcNow

                };

                await _dbContext.Problems.AddAsync(problem);
                await _dbContext.SaveChangesAsync();

                if (tags.Count() > 0)
                {
                    foreach (string tag in tags)
                    {
                        int tagId = await _tagRespository.GetIdByName(tag);

                        if (tagId != -1)
                        {
                            var problemTag = new ProblemTag
                            {
                                ProblemId = problem.Id,
                                TagId = tagId
                            };

                            await _dbContext.ProblemTags.AddAsync(problemTag);
                            await _dbContext.SaveChangesAsync();
                        }


                        Console.WriteLine("Addede to ProblemTag: {0} : {1}", problem.Id, tagId);
                    }

                }

                return problem;

            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task<bool> DeleteAsync(int id)
        {
            try
            {
                var problem = new Problem { Id = id };
                _dbContext.Problems.Attach(problem);
                _dbContext.Problems.Remove(problem);
                await _dbContext.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
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

        public async Task<bool> UpdateAsync(UpdateProblemReq req, int id)
        {
            try
            {
                var problem = await _dbContext.Problems.FindAsync(id) ?? throw new Exception("Discussion not found");
                problem.Title = req.Title;
                problem.Explaination = req.Explaination;
                problem.Difficulty = req.Difficulty;
                problem.FunctionName = req.FunctionName;
                problem.ReturnType = req.ReturnType;

                _dbContext.Problems.Update(problem);
                await _dbContext.SaveChangesAsync();

                return true;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
