using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using UIT.CodeRelax.UseCases.DTOs.Requests.Submission;
using UIT.CodeRelax.UseCases.DTOs.Responses.Submission;
using UIT.CodeRelax.UseCases.DTOs.Responses.Testcase;
using UIT.CodeRelax.UseCases.Services.Interfaces;

namespace UIT.CodeRelax.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SubmissionsController : ControllerBase
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
            return Ok(await _submissionService.Create(req));
        }


        /// <summary>
        /// GetByID
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [ProducesResponseType(200, Type = typeof(GetSubmissionRes))]
        [HttpGet("{id}")]
        public async Task<ActionResult> GetByID(int id)
        {
            var result = await _submissionService.GetByID(id);
            return Ok(result);
        }

        /// <summary>
        /// GetByID
        /// </summary>
        /// <returns></returns>
        [ProducesResponseType(200, Type = typeof(IEnumerable<GetSubmissionRes>))]
        [HttpGet]
        public async Task<ActionResult> GetAll()
        {
            var result = await _submissionService.GetAll();
            return Ok(result);
        }

        /// <summary>
        /// GetByID
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [ProducesResponseType(200, Type = typeof(bool))]
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            var result = await _submissionService.Delete(id);
            return Ok(result);
        }
    }
}
