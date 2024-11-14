using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UIT.CodeRelax.UseCases.DTOs.Requests.Discussion;
using UIT.CodeRelax.UseCases.DTOs.Responses.Discussion;

namespace UIT.CodeRelax.UseCases.Repositories
{
    public interface IDiscussionRepository
    {
        Task<bool> CreateAsync(CreateDiscussionReq req);
        Task<bool> UpdateAsync(UpdateDiscussionReq req);
        Task<bool> DeleteAsync(int id);
        Task<IEnumerable<DiscussionRes>> GetByProblemIDAsync(int problemID);
    }
}
