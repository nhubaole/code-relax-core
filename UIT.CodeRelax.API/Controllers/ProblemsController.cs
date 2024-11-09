using Microsoft.AspNetCore.Mvc;
using UIT.CodeRelax.UseCases.DTOs.Requests.Problem;
using UIT.CodeRelax.UseCases.DTOs.Responses.Problem;
using UIT.CodeRelax.UseCases.DTOs.Responses.Testcase;
using UIT.CodeRelax.UseCases.Services.Interfaces;

namespace UIT.CodeRelax.API.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class ProblemsController : ControllerBase
    {
        private readonly IProblemService _judgeService;

        public ProblemsController(IProblemService judgeService)
        {
            _judgeService = judgeService;
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
            return Ok(await _judgeService.GetTestCase(problemID));
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
            return Ok(await _judgeService.Submit(req));
        }
        [HttpGet("{id}")]
        public async Task<ActionResult> GetByID(int id)
        {
            var result = await _judgeService.GetByID(id);
            return Ok(result);
        }
    }
}
