using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UIT.CodeRelax.Core.Entities;
using UIT.CodeRelax.UseCases.DTOs.Requests;
using UIT.CodeRelax.UseCases.DTOs.Requests.Problem;
using UIT.CodeRelax.UseCases.DTOs.Responses;
using UIT.CodeRelax.UseCases.DTOs.Responses.Judge;
using UIT.CodeRelax.UseCases.DTOs.Responses.Problem;
using UIT.CodeRelax.UseCases.DTOs.Responses.Testcase;

namespace UIT.CodeRelax.UseCases.Services.Interfaces
{
    public interface IProblemService
    {
        Task<APIResponse<SubmitCodeRes>> Submit(SubmitCodeReq submitCodeReq);
        Task<IEnumerable<TestcaseRes>> GetTestCase(int problemID);

        Task<APIResponse<GetProblemRes>> CreateNewProblem(CreateProblemReq req);
    }
}
