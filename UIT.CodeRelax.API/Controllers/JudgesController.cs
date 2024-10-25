using Microsoft.AspNetCore.Mvc;
using UIT.CodeRelax.UseCases.DTOs.Responses.Problem;
using UIT.CodeRelax.UseCases.Services.Interfaces;

namespace UIT.CodeRelax.API.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class JudgesController : ControllerBase
    {
        private readonly IProblemService _judgeService;

        public JudgesController(IProblemService judgeService)
        {
            _judgeService = judgeService;
        }

        [HttpGet("[action]")]
        public async Task<IActionResult> GetTestcase(int problemID)
        {
            return Ok(await _judgeService.GetTestCase(problemID));
        }
    }
}
