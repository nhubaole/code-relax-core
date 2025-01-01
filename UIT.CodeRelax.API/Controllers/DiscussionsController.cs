using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using UIT.CodeRelax.UseCases.DTOs.Requests.Discussion;
using UIT.CodeRelax.UseCases.DTOs.Requests.Submission;
using UIT.CodeRelax.UseCases.DTOs.Responses.Discussion;
using UIT.CodeRelax.UseCases.DTOs.Responses.Submission;
using UIT.CodeRelax.UseCases.Services.Impls;
using UIT.CodeRelax.UseCases.Services.Interfaces;

namespace UIT.CodeRelax.API.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    //[Authorize]

    public class DiscussionsController : ControllerExtensions
    {
        private readonly IDiscussionService _discussionService;
        public DiscussionsController(IDiscussionService discussionService)
        {
            _discussionService = discussionService;
        }
        /// <summary>
        /// Create
        /// </summary>
        /// <param name="req"></param>
        /// <returns></returns>
        [ProducesResponseType(201, Type = typeof(bool))]
        [HttpPost()]
        public async Task<IActionResult> Create(CreateDiscussionReq req)
        {
            return ApiOK(await _discussionService.Create(req));
        }

        /// <summary>
        /// Update
        /// </summary>
        /// <param name="req"></param>
        /// <returns></returns>
        [ProducesResponseType(201, Type = typeof(bool))]
        [HttpPut()]
        public async Task<IActionResult> Update(UpdateDiscussionReq req)
        {
            return ApiOK(await _discussionService.Update(req));
        }

        /// <summary>
        /// Get By ID
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [ProducesResponseType(200, Type = typeof(DiscussionRes))]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetByProblemID(int id)
        {
            var result = await _discussionService.GetByID(id);
            return ApiOK(result);
        }

        /// <summary>
        /// Get By Problem ID
        /// </summary>
        /// <param name="problemID"></param>
        /// <returns></returns>
        [ProducesResponseType(200, Type = typeof(IEnumerable<DiscussionRes>))]
        [HttpGet("Problem/{problemID}")]
        public async Task<IActionResult> GetByID(int problemID)
        {
            var result = await _discussionService.GetByProblemID(problemID);
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
            var result = await _discussionService.Delete(id);
            return ApiOK(result);
        }
    }
}
