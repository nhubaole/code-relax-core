using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using UIT.CodeRelax.UseCases.DTOs.Requests.Submission;
using UIT.CodeRelax.UseCases.DTOs.Responses.Submission;
using UIT.CodeRelax.UseCases.DTOs.Responses.Testcase;
using UIT.CodeRelax.UseCases.Services.Interfaces;

namespace UIT.CodeRelax.API.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    [Authorize]

    public class SubmissionsController : ControllerExtensions
    {
        private readonly ISubmissionService _submissionService;

        public SubmissionsController(ISubmissionService submissionService)
        {
            _submissionService = submissionService;
        }

        /// <summary>
        /// Create
        /// </summary>
        /// <param name="req"></param>
        /// <returns></returns>
        [ProducesResponseType(200, Type = typeof(bool))]
        [HttpPost("[action]")]
        public async Task<IActionResult> Create(CreateSubmissionReq req)
        {
            return ApiOK(await _submissionService.Create(req));
        }


        /// <summary>
        /// GetByID
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [ProducesResponseType(200, Type = typeof(GetSubmissionRes))]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetByID(int id)
        {
            var result = await _submissionService.GetByID(id);
            return ApiOK(result);
        }

        /// <summary>
        /// Get Problem ID and User ID
        /// </summary>
        /// <param name="req"></param>
        /// <returns></returns>
        [ProducesResponseType(200, Type = typeof(IEnumerable<GetSubmissionRes>))]
        [HttpPost("get-by-problem-user")]
        public async Task<IActionResult> GetByProblemAndUserID(GetSubmissionByProblemAndUserReq req)
        {
            var result = await _submissionService.GetByProblemAndUserID(req);
            return ApiOK(result);
        }

        /// <summary>
        /// GetByID
        /// </summary>
        /// <returns></returns>
        [ProducesResponseType(200, Type = typeof(IEnumerable<GetSubmissionRes>))]
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var result = await _submissionService.GetAll();
            return ApiOK(result);
        }

        /// <summary>
        /// Delete
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [ProducesResponseType(200, Type = typeof(bool))]
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _submissionService.Delete(id);
            return ApiOK(result);
        }

        /// <summary>
        /// Get By User ID
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [ProducesResponseType(200, Type = typeof(IEnumerable<GetSubmissionRes>))]
        [HttpGet("User/{id}")]
        public async Task<IActionResult> GetByUserID(int id)
        {
            var result = await _submissionService.GetByUserID(id);
            return ApiOK(result);
        }


        /// <summary>
        /// Get By User ID
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [ProducesResponseType(200, Type = typeof(GetStatisticByUserRes))]
        [HttpGet("user-statistic/{id}")]
        public async Task<IActionResult> GetStatisticByUserID(int id)
        {
            var result = await _submissionService.GetStatisticByUserID(id);
            return ApiOK(result);
        }
    }
}
