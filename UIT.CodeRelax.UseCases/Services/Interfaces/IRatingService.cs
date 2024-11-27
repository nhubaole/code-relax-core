using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UIT.CodeRelax.UseCases.DTOs.Requests.Discussion;
using UIT.CodeRelax.UseCases.DTOs.Requests.Rating;
using UIT.CodeRelax.UseCases.DTOs.Responses;
using UIT.CodeRelax.UseCases.DTOs.Responses.Discussion;
using UIT.CodeRelax.UseCases.DTOs.Responses.Rating;

namespace UIT.CodeRelax.UseCases.Services.Interfaces
{
    public interface IRatingService
    {
        public Task<APIResponse<IEnumerable<RatingRes>>> GetByProblemID(int problemID);
        public Task<APIResponse<bool>> Create(CreateRatingReq req);
        public Task<APIResponse<bool>> Update(UpdateRatingReq req, int problemID, int userID);
    }
}
