using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UIT.CodeRelax.UseCases.DTOs.Requests.Discussion;
using UIT.CodeRelax.UseCases.DTOs.Requests.Rating;
using UIT.CodeRelax.UseCases.DTOs.Responses.Discussion;
using UIT.CodeRelax.UseCases.DTOs.Responses.Rating;

namespace UIT.CodeRelax.UseCases.Repositories
{
    public interface IRatingRepository
    {
        Task<bool> CreateAsync(CreateRatingReq req);
        Task<bool> UpdateAsync(UpdateRatingReq req, int problemID, int userID);
        Task<IEnumerable<RatingRes>> GetByProblemIDAsync(int problemID);
    }
}
