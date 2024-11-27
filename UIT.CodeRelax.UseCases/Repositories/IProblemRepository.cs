using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UIT.CodeRelax.Core.Entities;
using UIT.CodeRelax.UseCases.DTOs.Requests.Problem;
using UIT.CodeRelax.UseCases.DTOs.Responses.Problem;

namespace UIT.CodeRelax.UseCases.Repositories
{
    public interface IProblemRepository
    {
        public Task<IEnumerable<GetProblemRes>> GetAllAsync();
        public Task<GetProblemRes> GetByIDAsync(int id);
        public Task<bool> DeleteAsync(int id);
        public Task<bool> UpdateAsync(UpdateProblemReq req, int id);
        public Task<Problem> CreateNewProblem(Problem problemReq, List<string> tags);

        //public Task<IEumerable<GetProblemDashboardRes>>GetAllProDabAsync(int idUser);

    }
}
