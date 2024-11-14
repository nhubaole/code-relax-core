using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UIT.CodeRelax.Core.Entities;
using UIT.CodeRelax.UseCases.DTOs.Requests;
using UIT.CodeRelax.UseCases.DTOs.Requests.Problem;
using UIT.CodeRelax.UseCases.DTOs.Responses;
using UIT.CodeRelax.UseCases.DTOs.Responses.Problem;
using UIT.CodeRelax.UseCases.DTOs.Responses.Testcase;

namespace UIT.CodeRelax.UseCases.Services.Interfaces
{
    public interface IProblemService
    {
        Task<APIResponse<SubmitCodeRes>> Submit(SubmitCodeReq submitCodeReq);
        Task<APIResponse<SubmitCodeRes>> RunCode(SubmitCodeReq submitCodeReq);

        Task<APIResponse<IEnumerable<TestcaseRes>>> GetTestCase(int problemID);
        Task<APIResponse<GetProblemRes>> GetByID(int problemID);
        Task<APIResponse<IEnumerable<GetProblemRes>>> GetAll();

        Task<APIResponse<GetProblemRes>> CreateNewProblem(CreateProblemReq req);
    }
}
