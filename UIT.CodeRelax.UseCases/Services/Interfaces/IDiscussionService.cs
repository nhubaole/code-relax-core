using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UIT.CodeRelax.UseCases.DTOs.Requests.Discussion;
using UIT.CodeRelax.UseCases.DTOs.Requests.Submission;
using UIT.CodeRelax.UseCases.DTOs.Responses;
using UIT.CodeRelax.UseCases.DTOs.Responses.Discussion;
using UIT.CodeRelax.UseCases.DTOs.Responses.Submission;

namespace UIT.CodeRelax.UseCases.Services.Interfaces
{
    public interface IDiscussionService
    {
        public Task<APIResponse<IEnumerable<DiscussionRes>>> GetByProblemID(int problemID);
        public Task<APIResponse<DiscussionRes>> GetByID(int id);
        public Task<APIResponse<int>> Create(CreateDiscussionReq req);
        public Task<APIResponse<bool>> Update(UpdateDiscussionReq req);
        public Task<APIResponse<bool>> Delete(int id);
    }
}
