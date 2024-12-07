﻿using System;
using System.Collections.Generic;
using System.Linq;
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
        //private readonly ILogger<UserService> logger;


        public UserService(IUserRepository userRepository)
        {
            this.userRepository = userRepository;
        }

        private string errorMessage = null;

        public async Task<APIResponse<SignUpRes>> SignUp(SignUpReq signUpReq)
        {
            try
            {

                if (!IsValidEmail(signUpReq.Email))
                {
                    errorMessage = "Invalid email format.";
                }

                else if (!IsValidPassword(signUpReq.Password))
                {
                    errorMessage = "Password must be at least 8 characters long, contain at least one uppercase letter, one lowercase letter, and one digit."; // Lưu thông điệp lỗi cho mật khẩu
                }

                if (IsValidEmail(signUpReq.Email) && IsValidPassword(signUpReq.Password))
                {
                    var signUpRes = await userRepository.AddUserAsync(signUpReq);

                    if (signUpRes != null)
                    {
                        return new APIResponse<SignUpRes>
                        {
                            StatusCode = StatusCodeRes.ReturnWithData,
                            Message = "Success",
                            Data = new SignUpRes
                            {
                                Success = true,
                                DisplayName = signUpReq.DisplayName,
                                Password = signUpReq.Password,
                            }
                        };
                    }

                }

                return new APIResponse<SignUpRes>
                {
                    StatusCode = StatusCodeRes.InternalError,
                    Message = string.IsNullOrEmpty(errorMessage) ? "Not Success" : errorMessage,
                    Data = new SignUpRes
                    {
                        Success = false
                    }
                };

            }
            catch (Exception ex)
            {
                return new APIResponse<SignUpRes>
                {
                    StatusCode = StatusCodeRes.InternalError,
                    Message = ex.Message,
                    Data = new SignUpRes
                    {
                        Success = false
                    }
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
                var user = await userRepository.GetUserById(UserId);

                if (user != null)
                {
                    return new APIResponse<UserProfileRes>
                    {
                        StatusCode = StatusCodeRes.Success,
                        Message = "Success",
                        Data = new UserProfileRes
                        {
                            Success = true,
                            Id = UserId,
                            DisplayName = user.DisplayName,
                            Password = user.Password,
                            Email = user.Email,
                            Role = user.Role,
                            CreatedAt = user.CreatedAt

                        }
                    };
                }

                return new APIResponse<UserProfileRes>
                {
                    StatusCode = StatusCodeRes.InternalError,
                    Message = string.IsNullOrEmpty(errorMessage) ? "Not Success" : errorMessage,
                    Data = new UserProfileRes
                    {
                        Success = false
                    }
                };

            }
            catch (Exception ex)
            {
                return new APIResponse<UserProfileRes>
                {
                    StatusCode = StatusCodeRes.InternalError,
                    Message = ex.Message,
                    Data = new UserProfileRes
                    {
                        Success = false
                    }
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
                    return new APIResponse<LoginRes>
                    {
                        StatusCode = StatusCodeRes.Success,
                        Message = "Success",
                        Data = new LoginRes
                        {
                            Success = true,
                            UserProfile = user
                        }
                    };
                }

                return new APIResponse<LoginRes>
                {
                    StatusCode = StatusCodeRes.InternalError,
                    Message = string.IsNullOrEmpty(errorMessage) ? "Not Success" : errorMessage,
                    Data = new LoginRes
                    {
                        Success = false
                    }
                };

            }
            catch (Exception ex)
            {
                return new APIResponse<LoginRes>
                {
                    StatusCode = StatusCodeRes.InternalError,
                    Message = string.IsNullOrEmpty(errorMessage) ? "Not Success" : errorMessage,
                    Data = new LoginRes
                    {
                        Success = false
                    }
                };
                throw new Exception("UserSerrvice: An error occurred while  logging.\n", ex);
            }
            finally { errorMessage = null; }
        }

        public async Task<APIResponse<UserProfileRes>> UpdateUserProfile(UserProfileReq userProfileReq)
        {
            try
            {
                bool isExisted = (await userRepository.GetUserById(userProfileReq.Id)) != null;

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
                        User user = new User
                        {
                            Id = userProfileReq.Id,
                            Email = userProfileReq.Email,
                            Password = userProfileReq.Password,
                            DisplayName = userProfileReq.DisplayName,
                            Role = userProfileReq.Role,
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
                                    Success = true,
                                    Id = UpdatedUser.Id,
                                    DisplayName = UpdatedUser.DisplayName,
                                    Password = UpdatedUser.Password,
                                    Email = UpdatedUser.Email,
                                    Role = UpdatedUser.Role,
                                    CreatedAt = UpdatedUser.CreatedAt

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
                    Data = new UserProfileRes
                    {
                        Success = false
                    }
                };


            }
            catch (Exception ex)
            {
                return new APIResponse<UserProfileRes>
                {
                    StatusCode = StatusCodeRes.InternalError,
                    Message = ex.Message,
                    Data = new UserProfileRes
                    {
                        Success = false
                    }
                };
                throw new Exception("UserSerrvice: An error occurred while updating.\n", ex);
            }
            finally { errorMessage = null; }
        }
    }
}
