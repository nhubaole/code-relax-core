﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.IdentityModel.Tokens.Jwt;
using UIT.CodeRelax.Core.Entities;
using UIT.CodeRelax.UseCases.DTOs.Requests.Authentication;
using UIT.CodeRelax.UseCases.DTOs.Requests.User;
using UIT.CodeRelax.UseCases.DTOs.Responses;
using UIT.CodeRelax.UseCases.DTOs.Responses.Authentication;
using UIT.CodeRelax.UseCases.DTOs.Responses.User;
using UIT.CodeRelax.UseCases.Services.Interfaces;

namespace UIT.CodeRelax.API.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/v1/[controller]")]
    public class UserController : ControllerExtensions
    {
        private readonly IUserService userService;

        public UserController(IUserService userService)
        {
            this.userService = userService;
        }

        /// <summary>
        /// Lấy thông tin tài khoản bằng id
        /// </summary>
        /// <param name="UserId">Thông tin tài khoản</param>
        /// <returns>Thông tin tài khoản</returns>
        [HttpGet("{UserId}")]
        public async Task<IActionResult> GetUserByID(int UserId)
        {
            var response = await userService.GetUserById(UserId);
            return ApiOK(response);
        }


        /// <summary>
        /// Cập nhập thông tin tài khoản bằng id
        /// </summary>
        /// <param name="UserId">ID của tài khoản cần cập nhật.</param>
        /// <param name="user">Thông tin tài khoản mới để cập nhật.</param>
        /// <returns>Thông tin tài khoản đã được cập nhật.</returns>
        [HttpPut("{UserId}")]
        public async Task<IActionResult> UpdateUser(int UserId, [FromBody] UserProfileReq user)
        {

            var response = await userService.UpdateUserProfile(user);
            return ApiOK(response);
        }


        /// <summary>
        /// Lấy tất cả người dùng
        /// </summary>
        /// <returns>Danh sách người dùng.</returns>
        [HttpGet()]
        public async Task<IActionResult> GetAllUsers ()
        {
            var response = await userService.GetAllUser();
            return ApiOK(response);
        }

        /// <summary>
        /// Lấy người dùng hiện tại đã đăng nhập
        /// </summary>
        /// <returns>Thông tin người dùng.</returns>
        [HttpGet("CurrentUser")]
        public async Task<IActionResult> GetCurrentUser()
        {
            var email = User.Claims.FirstOrDefault(c => c.Type == "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/emailaddress")?.Value;
            if (email == null) {
                return Unauthorized("Can get current user. Please recheck token");
            }
            var response = await userService.GetCurrentUser(email);
            return ApiOK(response);
        }

    }
}
