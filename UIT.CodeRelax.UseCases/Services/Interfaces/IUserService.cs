using UIT.CodeRelax.UseCases.DTOs.Requests;
using UIT.CodeRelax.UseCases.DTOs.Requests.Authentication;
using UIT.CodeRelax.UseCases.DTOs.Requests.User;
using UIT.CodeRelax.UseCases.DTOs.Responses;
using UIT.CodeRelax.UseCases.DTOs.Responses.Authentication;
using UIT.CodeRelax.UseCases.DTOs.Responses.Judge;
using UIT.CodeRelax.UseCases.DTOs.Responses.Testcase;
using UIT.CodeRelax.UseCases.DTOs.Responses.User;

namespace UIT.CodeRelax.UseCases.Services.Interfaces
{
    public interface IUserService
    {
        Task<APIResponse<SignUpRes>> SignUp(SignUpReq signUpReq);
        Task<APIResponse<LoginRes>> Login(LoginReq loginReq);
        Task<APIResponse<UserProfileRes>> GetUserById(int UserId);

        Task<APIResponse<UserProfileRes>> UpdateUserProfile(UserProfileReq userProfileReq);
    }
}