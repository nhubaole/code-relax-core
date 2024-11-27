using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UIT.CodeRelax.UseCases.DTOs.Requests.Rating;
using UIT.CodeRelax.UseCases.DTOs.Responses;
using UIT.CodeRelax.UseCases.DTOs.Responses.Discussion;
using UIT.CodeRelax.UseCases.DTOs.Responses.Rating;
using UIT.CodeRelax.UseCases.Repositories;
using UIT.CodeRelax.UseCases.Services.Interfaces;

namespace UIT.CodeRelax.UseCases.Services.Impls
{
    public class RatingService : IRatingService
    {
        private readonly IRatingRepository _ratingRepository;
        public RatingService(IRatingRepository ratingRepository)
        {
            _ratingRepository = ratingRepository;
        }

        public async Task<APIResponse<bool>> Create(CreateRatingReq req)
        {
            try
            {
                var isCreated = await _ratingRepository.CreateAsync(req);

                return new APIResponse<bool>
                {
                    StatusCode = StatusCodeRes.ReturnWithData,
                    Message = "Success",
                    Data = isCreated
                };
            }
            catch (Exception ex)
            {
                return new APIResponse<bool>
                {
                    StatusCode = StatusCodeRes.InternalError,
                    Message = ex.Message,
                    Data = false
                };
            }
        }

        public async Task<APIResponse<IEnumerable<RatingRes>>> GetByProblemID(int problemID)
        {
            try
            {
                var discussions = await _ratingRepository.GetByProblemIDAsync(problemID);
                if (discussions == null || !discussions.Any())
                {
                    return new APIResponse<IEnumerable<RatingRes>>
                    {
                        StatusCode = StatusCodeRes.ResourceNotFound,
                        Message = "No data",
                        Data = null
                    };
                }
                return new APIResponse<IEnumerable<RatingRes>>
                {
                    StatusCode = StatusCodeRes.Success,
                    Message = "Success",
                    Data = discussions
                };
            }
            catch (Exception ex)
            {
                return new APIResponse<IEnumerable<RatingRes>>
                {
                    StatusCode = StatusCodeRes.InternalError,
                    Message = ex.Message,
                    Data = null
                };
            }
        }

        public async Task<APIResponse<bool>> Update(UpdateRatingReq req, int problemID, int userID)
        {
            try
            {
                var isCreated = await _ratingRepository.UpdateAsync(req, problemID, userID);

                return new APIResponse<bool>
                {
                    StatusCode = StatusCodeRes.ReturnWithData,
                    Message = "Success",
                    Data = isCreated
                };
            }
            catch (Exception ex)
            {
                return new APIResponse<bool>
                {
                    StatusCode = StatusCodeRes.InternalError,
                    Message = ex.Message,
                    Data = false
                };
            }
        }
    }
}
