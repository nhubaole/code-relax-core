using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UIT.CodeRelax.UseCases.DTOs.Requests.Discussion;
using UIT.CodeRelax.UseCases.DTOs.Responses;
using UIT.CodeRelax.UseCases.DTOs.Responses.Discussion;
using UIT.CodeRelax.UseCases.Repositories;
using UIT.CodeRelax.UseCases.Services.Interfaces;

namespace UIT.CodeRelax.UseCases.Services.Impls
{
    public class DiscussionService : IDiscussionService
    {
        private readonly IDiscussionRepository _discussionRepository;

        public DiscussionService(IDiscussionRepository discussionRepository)
        {
            _discussionRepository = discussionRepository;
        }

        public async Task<APIResponse<bool>> Create(CreateDiscussionReq req)
        {
            try
            {
                var isCreated = await _discussionRepository.CreateAsync(req);

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

        public async Task<APIResponse<bool>> Delete(int id)
        {
            try
            {
                var isDeleted = await _discussionRepository.DeleteAsync(id);

                return new APIResponse<bool>
                {
                    StatusCode = StatusCodeRes.Success,
                    Message = "Success",
                    Data = isDeleted
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

        public async Task<APIResponse<IEnumerable<DiscussionRes>>> GetByProblemID(int problemID)
        {
            try
            {
                var discussions = await _discussionRepository.GetByProblemIDAsync(problemID);
                if (discussions == null || !discussions.Any())
                {
                    return new APIResponse<IEnumerable<DiscussionRes>>
                    {
                        StatusCode = StatusCodeRes.ResourceNotFound,
                        Message = "No data",
                        Data = null
                    };
                }
                return new APIResponse<IEnumerable<DiscussionRes>>
                {
                    StatusCode = StatusCodeRes.Success,
                    Message = "Success",
                    Data = discussions
                };
            }
            catch (Exception ex)
            {
                return new APIResponse<IEnumerable<DiscussionRes>>
                {
                    StatusCode = StatusCodeRes.InternalError,
                    Message = ex.Message,
                    Data = null
                };
            }
        }

        public async Task<APIResponse<bool>> Update(UpdateDiscussionReq req)
        {
            try
            {
                var isCreated = await _discussionRepository.UpdateAsync(req);

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
