using Microsoft.AspNetCore.Mvc;
using UIT.CodeRelax.Core.Entities;
using UIT.CodeRelax.UseCases.DTOs.Requests.Authentication;
using UIT.CodeRelax.UseCases.DTOs.Responses;
using UIT.CodeRelax.UseCases.DTOs.Responses.Authentication;
using UIT.CodeRelax.UseCases.DTOs.Responses.User;
using UIT.CodeRelax.UseCases.Services.Interfaces;

namespace UIT.CodeRelax.API.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly IUserService userService;

        public UserController(IUserService userService)
        {
            this.userService = userService;
        }

        [HttpPost("Signup")]
        public async Task<IActionResult> SignUp(SignUpReq signUpReq)
        {
            return Ok(await userService.SignUp(signUpReq));
        }

        [HttpGet("{UserId}")]
        public async Task<IActionResult> GetUserByID(int UserId)
        {
            var response = await userService.GetUserById(UserId);

            if (response == null || !response.Data.Success)
            {
                return NotFound(new APIResponse<UserProfileRes>
                {
                    StatusCode = 404,
                    Message = "User not found",
                    Data = null
                });
            }

            return Ok(response);
        }
    }
}
