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
        public Task<IEnumerable<GetProblemRes>> GetAllAsync(int? userId);
        public Task<GetProblemRes> GetByIDAsync(int id, int? userId);
        public Task<bool> DeleteAsync(int id);
        public Task<bool> UpdateAsync(UpdateProblemReq req, int id);
        public Task<Problem> CreateProblemAsync(CreateProblemReq req);

        //public Task<IEumerable<GetProblemDashboardRes>>GetAllProDabAsync(int idUser);

    }
}
