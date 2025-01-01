using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
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

        public async Task<Problem> CreateProblemAsync(CreateProblemReq req)
        {
            using var transaction = await _dbContext.Database.BeginTransactionAsync();
            try
            {
                // Step 1: Create the Problem entity
                var problem = new Problem
                {
                    Title = req.Title,
                    Explaination = req.Explaination,
                    Difficulty = req.Difficulty,
                    FunctionName = req.FunctionName,
                    ReturnType = req.ReturnType,
                    CreatedAt = DateTime.UtcNow
                };

                // Add the problem to the database
                await _dbContext.Problems.AddAsync(problem);
                await _dbContext.SaveChangesAsync(); // Save to generate the Problem ID

                // Step 2: Handle Tags (if any exist)
                if (req.Tags?.Any() == true)
                {
                    foreach (string tagName in req.Tags)
                    {
                        int tagId = await _tagRespository.GetIdByName(tagName);

                        if (tagId != -1)
                        {
                            var problemTag = new ProblemTag
                            {
                                ProblemId = problem.Id,
                                TagId = tagId
                            };

                            await _dbContext.ProblemTags.AddAsync(problemTag);
                        }
                    }
                }

                // Step 3: Handle Testcases (if any exist)
                if (req.Input?.Any() == true && req.Output?.Any() == true && req.Input.Count == req.Output.Count)
                {
                    for (int i = 0; i < req.Input.Count; i++)
                    {
                        // Deserialize the input if necessary and convert it to the desired JSON format
                        var formattedInput = JsonSerializer.Deserialize<Dictionary<string, object>>(req.Input[i]);
                        var formattedOutput = JsonSerializer.Deserialize<object>(req.Output[i]);

                        var testcase = new Testcase
                        {
                            ProblemId = problem.Id,
                            Input = JsonSerializer.Serialize(formattedInput, new JsonSerializerOptions
                            {
                                WriteIndented = true // Ensures pretty formatting with indentation
                            }),
                            Output = JsonSerializer.Serialize(formattedOutput, new JsonSerializerOptions
                            {
                                WriteIndented = true
                            }),
                            IsExample = i < 3, // First three test cases are examples
                            CreatedAt = DateTime.UtcNow
                        };

                        await _dbContext.Testcases.AddAsync(testcase);
                    }
                }

                // Save all changes and commit the transaction
                await _dbContext.SaveChangesAsync();
                await transaction.CommitAsync();

                return problem;
            }
            catch (Exception ex)
            {
                // Rollback the transaction in case of an error
                await transaction.RollbackAsync();
                throw new Exception("Error while creating the problem", ex);
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

        public async Task<IEnumerable<GetProblemRes>> GetAllAsync(int? userId)
        {
            try
            {
                var problems = await _dbContext.Problems
                                    .Include(p => p.Testcases)
                                    .Include(p => p.ProblemTags)
                                        .ThenInclude(pt => pt.Tag)
                                    .Include(p => p.Submissions)
                                    .Include(p => p.ratings)
                                    .ToListAsync();

                var problemResponses = problems.Select(p => new GetProblemRes
                {
                    Id = p.Id,
                    Title = p.Title,
                    Explaination = p.Explaination,
                    FunctionName = p.FunctionName ?? string.Empty,
                    ReturnType = p.ReturnType ?? string.Empty,
                    Difficulty = p.Difficulty,
                    NumOfAcceptance = p.NumOfAcceptance,
                    NumOfSubmission = p.NumOfSubmission,
                    TotalTestCase = p.Testcases.Count,
                    Tag = p.ProblemTags.Select(pt => pt.Tag.Name).ToList(),
                    CreatedAt = p.CreatedAt,
                    AverageRating = p.ratings.Any() ? p.ratings.Average(r => r.NumberOfStar) : 0,
                    IsSolved = userId != null ? p.Submissions.Any(s => s.UserId == userId && s.Status == 1) : false
                });

                return problemResponses;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public async Task<GetProblemRes> GetByIDAsync(int id, int? userId)
        {
            try
            {
                var problem = await _dbContext.Problems
                                    .Include(p => p.Testcases)
                                   .Include(p => p.ProblemTags)
                                       .ThenInclude(pt => pt.Tag).Include(p => p.Submissions)
                                    .Include(p => p.ratings).FirstOrDefaultAsync(x => x.Id == id);
                var problemResponses = new GetProblemRes
                {
                    Id = problem.Id,
                    Title = problem.Title,
                    Explaination = problem.Explaination,
                    FunctionName = problem.FunctionName ?? string.Empty,
                    ReturnType = problem.ReturnType ?? string.Empty,
                    Difficulty = problem.Difficulty,
                    NumOfAcceptance = problem.NumOfAcceptance,
                    NumOfSubmission = problem.NumOfSubmission,
                    TotalTestCase = problem.Testcases.Count,
                    Tag = problem.ProblemTags.Select(pt => pt.Tag.Name).ToList(),
                    CreatedAt = problem.CreatedAt,
                    AverageRating = problem.ratings.Any() ? problem.ratings.Average(r => r.NumberOfStar) : 0,
                    IsSolved = userId != null ? problem.Submissions.Any(s => s.UserId == userId && s.Status == 1) : false
                };

                return problemResponses;
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
