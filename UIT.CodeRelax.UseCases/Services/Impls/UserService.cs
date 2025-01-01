using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using UIT.CodeRelax.Core.Entities;
using UIT.CodeRelax.UseCases.DTOs.Requests.Authentication;
using UIT.CodeRelax.UseCases.DTOs.Requests.User;
using UIT.CodeRelax.UseCases.DTOs.Responses;
using UIT.CodeRelax.UseCases.DTOs.Responses.Authentication;
using UIT.CodeRelax.UseCases.DTOs.Responses.User;
using UIT.CodeRelax.UseCases.Repositories;
using UIT.CodeRelax.UseCases.Services.Interfaces;

namespace UIT.CodeRelax.UseCases.Services.Impls
{
    public class UserService : IUserService
    {
        private readonly IUserRepository userRepository;
        private readonly IConfiguration _config;
        private readonly ILogger<UserService> logger;
        private readonly IStorageService _storageService;


        public UserService(IUserRepository userRepository, IConfiguration configuration, IStorageService storageService)
        {
            this.userRepository = userRepository;
            this._config = configuration;
            _storageService = storageService;   
        }

        private string errorMessage = null;

        public async Task<APIResponse<SignUpRes>> SignUp(SignUpReq signUpReq)
        {
            try
            {

                var signUpRes = await userRepository.AddUserAsync(signUpReq);

                if (signUpRes != null)
                {
                    return new APIResponse<SignUpRes>
                    {
                        StatusCode = StatusCodeRes.ReturnWithData,
                        Message = "Sign up successed. Please login",
                        Data = new SignUpRes
                        {
                            DisplayName = signUpReq.DisplayName,
                            Email = signUpReq.Email,
                        }
                    };
                }

                return new APIResponse<SignUpRes>
                {
                    StatusCode = StatusCodeRes.InternalError,
                    Message = string.IsNullOrEmpty(errorMessage) ? "Not Success" : errorMessage,
                    Data = null,
                };

            }
            catch (Exception ex)
            {
                return new APIResponse<SignUpRes>
                {
                    StatusCode = StatusCodeRes.InternalError,
                    Message = ex.Message,
                    Data = null,
                };
                throw new Exception("UserSerrvice: An error occurred while signing up.\n", ex);
            }
            finally { errorMessage = null; }
        }


        public bool IsValidEmail(string mail)
        {
            var emailRegex = @"^[^@\s]+@[^@\s]+\.[^@\s]+$";
            return Regex.IsMatch(mail, emailRegex);

        }

        public bool IsValidPassword(string pass)
        {
            return pass.Length > 8 && pass.Any(Char.IsUpper) && pass.Any(Char.IsLower) && pass.Any(Char.IsDigit);
        }

        public async Task<APIResponse<UserProfileRes>> GetUserById(int UserId)
        {
            try
            {
                var user = await userRepository.GetUserByIdAsync(UserId);

                if (user != null)
                {
                    return new APIResponse<UserProfileRes>
                    {
                        StatusCode = StatusCodeRes.Success,
                        Message = "Success",
                        Data = new UserProfileRes
                        {
                            Id = UserId,
                            DisplayName = user.DisplayName,
                            Password = user.Password,
                            Email = user.Email,
                            Role = user.Role,
                            CreatedAt = user.CreatedAt,
                            AvatarUrl = user.AvatarUrl,
                            Facebook = user.Facebook,
                            Github = user.Github,
                            Google = user.Google,
                        }
                    };
                }

                return new APIResponse<UserProfileRes>
                {
                    StatusCode = StatusCodeRes.InternalError,
                    Message = string.IsNullOrEmpty(errorMessage) ? "Not Success" : errorMessage,
                };

            }
            catch (Exception ex)
            {
                return new APIResponse<UserProfileRes>
                {
                    StatusCode = StatusCodeRes.InternalError,
                    Message = ex.Message,
                };
                throw new Exception("UserSerrvice: An error occurred while signing up.\n", ex);
            }
            finally { errorMessage = null; }
        }

        public async Task<APIResponse<LoginRes>> Login(LoginReq loginReq)
        {
            try
            {
                var user = await userRepository.AuthorizeUser(loginReq);

                if (user != null)
                {
                    string jwt = GenerateJwtToken(user);

                    return new APIResponse<LoginRes>
                    {
                        StatusCode = StatusCodeRes.Success,
                        Message = "Success",
                        Data = new LoginRes
                        {
                            Token = jwt,
                            ExpiresIn =(int)(DateTime.UtcNow.AddHours(1) - DateTime.UtcNow).TotalSeconds
                        }
                    };
                }
                
                return new APIResponse<LoginRes>
                {
                    StatusCode = StatusCodeRes.InvalidData,
                    Message = "User is not exsited. Please check email and password",
                };

            }
            catch (Exception ex)
            {
                return new APIResponse<LoginRes>
                {
                    StatusCode = StatusCodeRes.InternalError,
                    Message = string.IsNullOrEmpty(errorMessage) ? "Not Success" : errorMessage,

                };
                throw new Exception("UserSerrvice: An error occurred while  logging.\n", ex);
            }
            finally { errorMessage = null; }
        }

        public async Task<APIResponse<UserProfileRes>> UpdateUserProfile(UserProfileReq userProfileReq)
        {
            try
            {
                bool isExisted = (await userRepository.GetUserByIdAsync(userProfileReq.Id)) != null;

                if (isExisted)
                {
                    if (!IsValidEmail(userProfileReq.Email))
                    {
                        errorMessage = "Invalid email format.";
                    }

                    else if (!IsValidPassword(userProfileReq.Password))
                    {
                        errorMessage = "Password must be at least 8 characters long, contain at least one uppercase letter, one lowercase letter, and one digit."; // Lưu thông điệp lỗi cho mật khẩu
                    }

                    if (String.IsNullOrEmpty(errorMessage))
                    {

                        var avatarUrl = await _storageService.Upload(userProfileReq.formFile, "users-avatar", userProfileReq.Id);

                        User user = new User
                        {
                            Id = userProfileReq.Id,
                            Email = userProfileReq.Email,
                            Password = userProfileReq.Password,
                            DisplayName = userProfileReq.DisplayName,
                            Role = userProfileReq.Role,
                            Facebook = userProfileReq.Facebook,
                            Github = userProfileReq.Github,
                            Google = userProfileReq.Google,
                            AvatarUrl = avatarUrl.Data != null ? avatarUrl?.Data : null,
                            CreatedAt = DateTime.UtcNow
                        };

                        
                        var UpdatedUser = await userRepository.UpdateUserAsync(user);

                        if (UpdatedUser != null)
                        {
                            return new APIResponse<UserProfileRes>
                            {
                                StatusCode = StatusCodeRes.Success,
                                Message = "Success",
                                Data = new UserProfileRes
                                {
                                    Id = UpdatedUser.Id,
                                    DisplayName = UpdatedUser.DisplayName,
                                    Password = UpdatedUser.Password,
                                    Email = UpdatedUser.Email,
                                    Role = UpdatedUser.Role,
                                    CreatedAt = UpdatedUser.CreatedAt,
                                    AvatarUrl = UpdatedUser.AvatarUrl,
                                    Facebook = UpdatedUser.Facebook,
                                    Github = UpdatedUser.Github,
                                    Google = UpdatedUser.Google,
                                }
                            };
                        }
                    }

                }
                else
                {
                    errorMessage = "User is not existed";
                }


                return new APIResponse<UserProfileRes>
                {
                    StatusCode = StatusCodeRes.InternalError,
                    Message = string.IsNullOrEmpty(errorMessage) ? "Not Success" : errorMessage,
                };


            }
            catch (Exception ex)
            {
                return new APIResponse<UserProfileRes>
                {
                    StatusCode = StatusCodeRes.InternalError,
                    Message = ex.Message,

                };
                throw new Exception("UserSerrvice: An error occurred while updating.\n", ex);
            }
            finally { errorMessage = null; }
        }

        public async Task<APIResponse<IEnumerable<UserProfileRes>>> GetAllUser()
        {
            try
            {
                var rs = await userRepository.GetAllUsersAsync();

                return new APIResponse<IEnumerable<UserProfileRes>>()
                {
                    StatusCode = StatusCodeRes.Success,
                    Message = "Success",
                    Data = rs,
                };
            }
            catch (Exception ex)
            {
                return new APIResponse<IEnumerable<UserProfileRes>>()
                {
                    StatusCode = StatusCodeRes.InternalError,
                    Message = ex.Message,
                    Data = null,
                };
            }
        }

        public string GenerateJwtToken(UserProfileRes loginReq)
        {
            var claims = new Dictionary<string, object>
            {
                { JwtRegisteredClaimNames.Email, loginReq.Email },
                { JwtRegisteredClaimNames.Name, loginReq.DisplayName }
            };

            var issuer = _config["Jwt:Issuer"];
            var audience = _config["Jwt:Audience"];
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:SecretKey"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var token = new SecurityTokenDescriptor
            {
                Issuer = issuer,
                Audience = audience,
                Claims = claims,
                SigningCredentials = credentials,
                Expires = DateTime.UtcNow.AddHours(1)
            };
            var securityToken = new JwtSecurityTokenHandler().CreateToken(token);


            return new JwtSecurityTokenHandler().WriteToken(securityToken);

        }

        public async Task<APIResponse<UserProfileRes>> GetCurrentUser(string email)
        {
            try
            {
                var user = await userRepository.GetUserByEmailAsync(email);

                if (user != null)
                {
                    return new APIResponse<UserProfileRes>
                    {
                        StatusCode = StatusCodeRes.Success,
                        Message = "Success",
                        Data = new UserProfileRes
                        {
                            Id = user.Id,
                            DisplayName = user.DisplayName,
                            Password = user.Password,
                            Email = user.Email,
                            Role = user.Role,
                            CreatedAt = user.CreatedAt,
                            AvatarUrl = user.AvatarUrl,
                            Facebook = user.Facebook,
                            Github = user.Github,
                            Google = user.Google,

                        }
                    };
                }

                return new APIResponse<UserProfileRes>
                {
                    StatusCode = StatusCodeRes.InternalError,
                    Message = string.IsNullOrEmpty(errorMessage) ? "Not Success" : errorMessage,
                };
            }
            catch (Exception ex) {
                return new APIResponse<UserProfileRes>
                {
                    StatusCode = StatusCodeRes.InternalError,
                    Message = string.IsNullOrEmpty(errorMessage) ? "Not Success" : ex.Message,
                };
            }
        }
    }
}
