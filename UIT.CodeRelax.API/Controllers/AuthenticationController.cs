using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using UIT.CodeRelax.UseCases.DTOs.Requests.Authentication;
using UIT.CodeRelax.UseCases.Services.Interfaces;

namespace UIT.CodeRelax.API.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class AuthenticationController : ControllerExtensions
    {
        private readonly IUserService userService;

        public AuthenticationController(IUserService userService)
        {
            this.userService = userService;
        }

        /// <summary>
        /// Đăng ký tài khoản mới.
        /// </summary>
        /// <param name="signUpReq">Thông tin yêu cầu đăng ký</param>
        /// <returns>Kết quả đăng ký tài khoản</returns>
        [AllowAnonymous]
        [HttpPost("Signup")]
        public async Task<IActionResult> SignUp(SignUpReq signUpReq)
        {
            return ApiOK(await userService.SignUp(signUpReq));
        }


        /// <summary>
        /// Đăng nhập tài khoản.
        /// </summary>
        /// <param name="loginReq">Thông tin đăng nhập</param>
        /// <returns>Kết quả đăng nhập</returns>
        [AllowAnonymous]
        [HttpPost("Login")]
        public async Task<IActionResult> Login(LoginReq loginReq)
        {
            var response = await userService.Login(loginReq);
            return ApiOK(response);

        }
    }
}
