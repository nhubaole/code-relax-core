using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UIT.CodeRelax.UseCases.DTOs.Requests.Submission;
using UIT.CodeRelax.UseCases.DTOs.Responses;
using UIT.CodeRelax.UseCases.DTOs.Responses.Submission;

namespace UIT.CodeRelax.UseCases.Services.Interfaces
{
    public interface ISubmissionService
    {

        public Task<APIResponse<IEnumerable<GetSubmissionRes>>> GetAll();
        public Task<APIResponse<GetSubmissionRes>> GetByID(int id);
        public Task<APIResponse<bool>> Create(CreateSubmissionReq req);
        public Task<APIResponse<bool>> Update(CreateSubmissionReq req);
        public Task<APIResponse<bool>> Delete(int id);
    }
}
