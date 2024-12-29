using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UIT.CodeRelax.Core.Entities;
using UIT.CodeRelax.Infrastructure.DataAccess;
using UIT.CodeRelax.UseCases.DTOs.Requests.Discussion;
using UIT.CodeRelax.UseCases.DTOs.Responses.Discussion;
using UIT.CodeRelax.UseCases.DTOs.Responses.Submission;
using UIT.CodeRelax.UseCases.Repositories;

namespace UIT.CodeRelax.Infrastructure.Repositories
{
    public class DiscussionRepository : IDiscussionRepository
    {
        private readonly AppDbContext _dbContext;
        private readonly IMapper _mapper;
        public DiscussionRepository(AppDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public async Task<int> CreateAsync(CreateDiscussionReq req)
        {

            try
            {
                var discussion = new Discussion
                {
                    Content = req.Content,
                    Type = req.Type,
                    UserID = req.UserID,
                    ProblemID = req.ProblemID,
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow,
                };

                await _dbContext.Discussions.AddAsync(discussion);
                await _dbContext.SaveChangesAsync();

                return discussion.ID;
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
                var discussion = new Discussion { ID = id };
                _dbContext.Discussions.Attach(discussion);
                _dbContext.Discussions.Remove(discussion);
                await _dbContext.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<DiscussionRes> GetByIdAsync(int id)
        {
            try
            {
                var discussion = await _dbContext.Discussions
                                        .Include(s => s.User)
                                        .FirstOrDefaultAsync(x => x.ID == id);
                return _mapper.Map<DiscussionRes>(discussion);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<IEnumerable<DiscussionRes>> GetByProblemIDAsync(int problemID)
        {
            try
            {
                var discussion = await _dbContext.Discussions
                                        .Include(s => s.User)
                                        .Where(x => x.ProblemID == problemID)
                                        .ToListAsync();
                return _mapper.Map<List<DiscussionRes>>(discussion);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<bool> UpdateAsync(UpdateDiscussionReq req)
        {
            try
            {
                var discussion = await _dbContext.Discussions.FindAsync(req.ID) ?? throw new Exception("Discussion not found");
                discussion.Content = req.Content;
                discussion.Type = req.Type;
                discussion.UpdatedAt = DateTime.UtcNow;

                _dbContext.Discussions.Update(discussion);
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
