using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using UIT.CodeRelax.UseCases.DTOs.Requests.Discussion;
using UIT.CodeRelax.UseCases.DTOs.Requests.Rating;
using UIT.CodeRelax.UseCases.DTOs.Responses.Discussion;
using UIT.CodeRelax.UseCases.DTOs.Responses.Rating;
using UIT.CodeRelax.UseCases.Services.Impls;
using UIT.CodeRelax.UseCases.Services.Interfaces;

namespace UIT.CodeRelax.API.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class RatingsController : ControllerExtensions
    {
        private readonly IRatingService _ratingService;
        public RatingsController(IRatingService ratingService)
        {
            _ratingService = ratingService;
        }
        /// <summary>
        /// Create
        /// </summary>
        /// <param name="req"></param>
        /// <returns></returns>
        [ProducesResponseType(200, Type = typeof(bool))]
        [HttpPost()]
        public async Task<IActionResult> Create(CreateRatingReq req)
        {
            return ApiOK(await _ratingService.Create(req));
        }

        /// <summary>
        /// Create
        /// </summary>
        /// <param name="req"></param>
        /// <param name="problemID"></param>
        /// <returns></returns>
        [ProducesResponseType(200, Type = typeof(bool))]
        [HttpPut("{problemID}")]
        public async Task<IActionResult> Update(UpdateRatingReq req, int problemID)
        {
            return ApiOK(await _ratingService.Update(req, problemID, 3));
        }

        /// <summary>
        /// Get By Problem ID
        /// </summary>
        /// <param name="problemID"></param>
        /// <returns></returns>
        [ProducesResponseType(200, Type = typeof(IEnumerable<RatingRes>))]
        [HttpGet("Problem/{problemID}")]
        public async Task<IActionResult> GetByProblemID(int problemID)
        {
            var result = await _ratingService.GetByProblemID(problemID);
            return ApiOK(result);
        }


    }
}
