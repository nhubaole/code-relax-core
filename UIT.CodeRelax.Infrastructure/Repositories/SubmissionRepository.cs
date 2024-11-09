using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UIT.CodeRelax.Core.Entities;
using UIT.CodeRelax.Infrastructure.DataAccess;
using UIT.CodeRelax.UseCases.DTOs.Requests.Authentication;
using UIT.CodeRelax.UseCases.DTOs.Requests.Submission;
using UIT.CodeRelax.UseCases.DTOs.Responses.Problem;
using UIT.CodeRelax.UseCases.DTOs.Responses.Submission;
using UIT.CodeRelax.UseCases.Repositories;

namespace UIT.CodeRelax.Infrastructure.Repositories
{
    public class SubmissionRepository : ISubmissionRepository
    {
        private readonly AppDbContext _dbContext;
        private readonly IMapper _mapper;
        public SubmissionRepository(AppDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public async Task<bool> CreateAsync(CreateSubmissionReq req)
        {
            try
            {
                var submission = new Submission
                {
                    ProblemId = req.ProblemID,
                    UserId = req.UserID,
                    Code = req.Code,
                    Language = (int)req.Language,
                    CreatedAt = DateTime.UtcNow
                };

                await _dbContext.Submissions.AddAsync(submission);
                await _dbContext.SaveChangesAsync();

                return true;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<bool> DeleteAsync(int id)
        {
            try
            {
                var submission = new Submission { Id = id };
                _dbContext.Submissions.Attach(submission);
                _dbContext.Submissions.Remove(submission);
                await _dbContext.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<IEnumerable<GetSubmissionRes>> GetAllAsync()
        {
            try
            {
                var submissions = await _dbContext.Submissions.ToArrayAsync();

                return _mapper.Map<IEnumerable<GetSubmissionRes>>(submissions);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<GetSubmissionRes> GetByIDAsync(int id)
        {
            try
            {
                var submission = await _dbContext.Submissions.FirstOrDefaultAsync(x => x.Id == id);
                return _mapper.Map<GetSubmissionRes>(submission);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public Task<bool> UpdateAsync(CreateSubmissionReq req)
        {
            throw new NotImplementedException();
        }
    }
}
