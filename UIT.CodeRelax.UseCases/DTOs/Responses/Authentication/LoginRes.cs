using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UIT.CodeRelax.UseCases.DTOs.Responses.User;

namespace UIT.CodeRelax.UseCases.DTOs.Responses.Authentication
{
    public class LoginRes
    {
        public string Token { get; set; }
        public UserProfileRes UserProfile { get; set; }
        public int ExpiresIn { get; set; }

        public LoginRes() { }
        public LoginRes(UserProfileRes userProfile, string token, int expiresIn)
        {
            this.UserProfile = userProfile;
            this.Token = token;
            ExpiresIn = expiresIn;
        }

    }
}
