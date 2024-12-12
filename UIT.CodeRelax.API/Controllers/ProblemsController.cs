using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using UIT.CodeRelax.UseCases.DTOs.Requests;
using UIT.CodeRelax.UseCases.DTOs.Requests.Problem;
using UIT.CodeRelax.UseCases.DTOs.Responses.Problem;
using UIT.CodeRelax.UseCases.DTOs.Responses.Testcase;
using UIT.CodeRelax.UseCases.Services.Impls;
using UIT.CodeRelax.UseCases.Services.Interfaces;

namespace UIT.CodeRelax.API.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    [Authorize]

    public class ProblemsController : ControllerExtensions
    {
        private readonly IProblemService _problemService;

        public ProblemsController(IProblemService problemService)
        {
            _problemService = problemService;
        }

        /// <summary>
        /// GetTestcase
        /// </summary>
        /// <param name="problemID"></param>
        /// <returns></returns>
        [ProducesResponseType(200, Type = typeof(IEnumerable<TestcaseRes>))]
        [HttpGet("[action]")]
        public async Task<IActionResult> GetTestcase(int problemID)
        {
            return ApiOK(await _problemService.GetTestCase(problemID));
        }

        /// <summary>
        /// Submit
        /// </summary>
        /// <param name="req"></param>
        /// <returns></returns>
        [ProducesResponseType(200, Type = typeof(SubmitCodeRes))]
        [HttpPost("[action]")]
        public async Task<IActionResult> Submit(SubmitCodeReq req)
        {
            return ApiOK(await _problemService.Submit(req));
        }

        /// <summary>
        /// Run Code
        /// </summary>
        /// <param name="req"></param>
        /// <returns></returns>
        [ProducesResponseType(200, Type = typeof(SubmitCodeRes))]
        [HttpPost("[action]")]
        public async Task<IActionResult> RunCode(SubmitCodeReq req)
        {
            return ApiOK(await _problemService.RunCode(req));
        }

        /// <summary>
        /// Get By ID
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [ProducesResponseType(200, Type = typeof(GetProblemRes))]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetByID(int id)
        {
            var result = await _problemService.GetByID(id);
            return ApiOK(result);
        }


        /// <summary>
        /// Get By ID
        /// </summary>
        /// <returns></returns>
        [ProducesResponseType(200, Type = typeof(IEnumerable<GetProblemRes>))]
        [HttpGet()]
        public async Task<IActionResult> GetAll()
        {
            var result = await _problemService.GetAll();
            return ApiOK(result);
        }

        [HttpPost("[action]")]
        public async Task<IActionResult> CreateProblem([FromBody] CreateProblemReq req)
        {

            return ApiOK(await _problemService.CreateNewProblem(req));

        }


    }
}
