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
        private readonly IUserService _userService;
        public ProblemsController(IProblemService problemService, IUserService userService)
        {
            _problemService = problemService;
            _userService = userService;
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
            var email = User.Claims.FirstOrDefault(c => c.Type == "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/emailaddress")?.Value;
            if (email == null)
            {
                return Unauthorized("Can get current user. Please recheck token");
            }
            var user = await _userService.GetCurrentUser(email);
            var result = await _problemService.GetByID(id, user.Data.Id);
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
            var email = User.Claims.FirstOrDefault(c => c.Type == "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/emailaddress")?.Value;
            if (email == null)
            {
                return Unauthorized("Can get current user. Please recheck token");
            }
            var user = await _userService.GetCurrentUser(email);
            var result = await _problemService.GetAll(user.Data.Id);
            return ApiOK(result);
        }

        /// <summary>
        /// Delete
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [ProducesResponseType(200, Type = typeof(IEnumerable<bool>))]
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _problemService.Delete(id);
            return ApiOK(result);
        }

        /// <summary>
        /// Update
        /// </summary>
        /// <param name="req"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        [ProducesResponseType(200, Type = typeof(IEnumerable<bool>))]
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(UpdateProblemReq req, int id)
        {
            var result = await _problemService.Update(req, id);
            return ApiOK(result);
        }

        [HttpPost("[action]")]
        /// <summary>
        /// Update
        /// </summary>
        /// <param name="req"></param>
        /// <returns></returns>
        public async Task<IActionResult> CreateProblem(CreateProblemReq req)
        {
            return ApiOK(await _problemService.CreateProblem(req));

        }


    }
}
