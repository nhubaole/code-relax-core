using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UIT.CodeRelax.Core.Entities;
using UIT.CodeRelax.UseCases.DTOs.Requests.Submission;
using UIT.CodeRelax.UseCases.DTOs.Responses;
using UIT.CodeRelax.UseCases.DTOs.Responses.Submission;
using UIT.CodeRelax.UseCases.DTOs.Responses.Testcase;
using UIT.CodeRelax.UseCases.Repositories;
using UIT.CodeRelax.UseCases.Services.Interfaces;

namespace UIT.CodeRelax.UseCases.Services.Impls
{
    public class SubmissionService : ISubmissionService
    {
        private readonly ISubmissionRepository _submissionRepository;
        public SubmissionService(ISubmissionRepository submissionRepository)
        {
            _submissionRepository = submissionRepository;
        }

        public async Task<APIResponse<bool>> Create(CreateSubmissionReq req)
        {
            try
            {
                var isCreated = await _submissionRepository.CreateAsync(req);

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
                var isDeleted = await _submissionRepository.DeleteAsync(id);

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

        public async Task<APIResponse<IEnumerable<GetSubmissionRes>>> GetAll()
        {
            try
            {
                var submissions = await _submissionRepository.GetAllAsync();

                return new APIResponse<IEnumerable<GetSubmissionRes>>
                {
                    StatusCode = StatusCodeRes.Success,
                    Message = "Success",
                    Data = submissions
                };
            }
            catch (Exception ex)
            {
                return new APIResponse<IEnumerable<GetSubmissionRes>>
                {
                    StatusCode = StatusCodeRes.InternalError,
                    Message = ex.Message,
                    Data = null
                };
            }
        }

        public async Task<APIResponse<GetSubmissionRes>> GetByID(int id)
        {
            try
            {
                var submission = await _submissionRepository.GetByIDAsync(id);

                return new APIResponse<GetSubmissionRes>
                {
                    StatusCode = StatusCodeRes.Success,
                    Message = "Success",
                    Data = submission
                };
            }
            catch (Exception ex)
            {
                return new APIResponse<GetSubmissionRes>
                {
                    StatusCode = StatusCodeRes.InternalError,
                    Message = ex.Message,
                    Data = null
                };
            }
        }

        public async Task<APIResponse<IEnumerable<GetSubmissionRes>>> GetByProblemAndUserID(GetSubmissionByProblemAndUserReq req)
        {
            try
            {
                var submission = await _submissionRepository.GetByProblemAndUserIDAsync(req);

                return new APIResponse<IEnumerable<GetSubmissionRes>>
                {
                    StatusCode = StatusCodeRes.Success,
                    Message = "Success",
                    Data = submission
                };
            }
            catch (Exception ex)
            {
                return new APIResponse<IEnumerable<GetSubmissionRes>>
                {
                    StatusCode = StatusCodeRes.InternalError,
                    Message = ex.Message,
                    Data = null
                };
            }
        }

        public async Task<APIResponse<IEnumerable<GetSubmissionRes>>> GetByUserID(int id)
        {
            try
            {
                var submission = await _submissionRepository.GetByUserIDAsync(id);

                return new APIResponse<IEnumerable<GetSubmissionRes>>
                {
                    StatusCode = StatusCodeRes.Success,
                    Message = "Success",
                    Data = submission
                };
            }
            catch (Exception ex)
            {
                return new APIResponse<IEnumerable<GetSubmissionRes>>
                {
                    StatusCode = StatusCodeRes.InternalError,
                    Message = ex.Message,
                    Data = null
                };
            }
        }

        public async Task<APIResponse<GetStatisticByUserRes>> GetStatisticByUserID(int id)
        {
            try
            {
                var metric = await _submissionRepository.GetStatisticByUserIDAsync(id);

                return new APIResponse<GetStatisticByUserRes>
                {
                    StatusCode = StatusCodeRes.Success,
                    Message = "Success",
                    Data = metric
                };
            }
            catch (Exception ex)
            {
                return new APIResponse<GetStatisticByUserRes>
                {
                    StatusCode = StatusCodeRes.InternalError,
                    Message = ex.Message,
                    Data = null
                };
            }
        }

        public Task<APIResponse<bool>> Update(CreateSubmissionReq req)
        {
            throw new NotImplementedException();
        }
    }
}
