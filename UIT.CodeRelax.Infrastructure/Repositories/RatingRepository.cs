using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UIT.CodeRelax.Core.Entities;
using UIT.CodeRelax.Infrastructure.DataAccess;
using UIT.CodeRelax.UseCases.DTOs.Requests.Rating;
using UIT.CodeRelax.UseCases.DTOs.Responses.Discussion;
using UIT.CodeRelax.UseCases.DTOs.Responses.Rating;
using UIT.CodeRelax.UseCases.Repositories;

namespace UIT.CodeRelax.Infrastructure.Repositories
{
    public class RatingRepository : IRatingRepository
    {
        private readonly AppDbContext _dbContext;
        private readonly IMapper _mapper;

        public RatingRepository(AppDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }
        public async Task<bool> CreateAsync(CreateRatingReq req)
        {
            try
            {
                var rating = new Rating
                {
                    UserID = req.UserID,
                    ProblemID = req.ProblemID,
                    NumberOfStar = req.NumberOfStar,
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow,
                };

                await _dbContext.Ratings.AddAsync(rating);
                await _dbContext.SaveChangesAsync();

                return true;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<bool> UpdateAsync(UpdateRatingReq req, int problemID, int userID)
        {
            try
            {
                var rating = await _dbContext.Ratings.Where(x => x.ProblemID == problemID && x.UserID == userID).FirstAsync() ?? throw new Exception("Discussion not found");
                rating.NumberOfStar = req.NumberOfStar;
                rating.UpdatedAt = DateTime.UtcNow;

                _dbContext.Ratings.Update(rating);
                await _dbContext.SaveChangesAsync();

                return true;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<IEnumerable<RatingRes>> GetByProblemIDAsync(int problemID)
        {
            try
            {
                var rating = await _dbContext.Ratings
                                        .Include(s => s.User)
                                        .Where(x => x.ProblemID == problemID)
                                        .ToListAsync();
                return _mapper.Map<List<RatingRes>>(rating);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
