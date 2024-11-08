using Microsoft.AspNetCore.Mvc;
using UIT.CodeRelax.UseCases.DTOs.Requests.Problem;
using UIT.CodeRelax.UseCases.Services.Impls;
using UIT.CodeRelax.UseCases.Services.Interfaces;

namespace UIT.CodeRelax.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProblemController : ControllerBase
    {
        private readonly IProblemService _problemService;

        public ProblemController(IProblemService problemService)
        {
            _problemService = problemService;
        }

        [HttpPost]
        public async Task<IActionResult> CreateProblem([FromBody] CreateProblemReq req)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            return Ok(await _problemService.CreateNewProblem(req));

        }


    }
}
