﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UIT.CodeRelax.UseCases.DTOs.Requests.Authentication;
using UIT.CodeRelax.UseCases.DTOs.Requests.User;
using UIT.CodeRelax.UseCases.DTOs.Responses.Authentication;
using UIT.CodeRelax.UseCases.DTOs.Responses.Testcase;
using UIT.CodeRelax.UseCases.DTOs.Responses.User;

namespace UIT.CodeRelax.UseCases.Repositories
{
    public interface IUserRepository
    {
        //Task<IEnumerable<UserRes>> GetUserById(int UserId);

        Task<SignUpRes> SignUpAsync(SignUpReq signUpReq);
    }
}
