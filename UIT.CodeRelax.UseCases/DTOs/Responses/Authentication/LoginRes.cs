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
        public string AccessToken { get; set; }
        public UserProfileRes UserProfile { get; set; }

    }
}
