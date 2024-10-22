using Microsoft.AspNetCore.Mvc;
using UIT.CodeRelax.UseCases.DTOs.Responses;
using UIT.CodeRelax.UseCases.Services.Interfaces;

namespace UIT.CodeRelax.API.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private readonly IJudgeService _judgeService;

        public WeatherForecastController(IJudgeService judgeService)
        {
            _judgeService = judgeService;
        }

        [HttpGet("[action]")]
        public GetProblemRes GetAllProblem()
        {
            return _judgeService.GetProblems();
        }
    }
}
