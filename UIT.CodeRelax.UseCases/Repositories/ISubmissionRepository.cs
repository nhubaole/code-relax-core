using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UIT.CodeRelax.UseCases.DTOs.Requests.Submission;
using UIT.CodeRelax.UseCases.DTOs.Responses.Problem;
using UIT.CodeRelax.UseCases.DTOs.Responses.Submission;

namespace UIT.CodeRelax.UseCases.Repositories
{
    public interface ISubmissionRepository
    {
        public Task<IEnumerable<GetSubmissionRes>> GetAllAsync();
        public Task<IEnumerable<GetSubmissionRes>> GetByUserIDAsync(int id);
        public Task<IEnumerable<GetSubmissionRes>> GetByProblemAndUserIDAsync(GetSubmissionByProblemAndUserReq req);
        public Task<GetSubmissionRes> GetByIDAsync(int id);
        public Task<GetStatisticByUserRes> GetStatisticByUserIDAsync(int id);
        public Task<bool> CreateAsync(CreateSubmissionReq req);
        public Task<bool> UpdateAsync(CreateSubmissionReq req);
        public Task<bool> DeleteAsync(int id);
    }
}
