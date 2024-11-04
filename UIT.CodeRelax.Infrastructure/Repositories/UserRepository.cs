using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UIT.CodeRelax.Core.Entities;
using UIT.CodeRelax.Infrastructure.DataAccess;
using UIT.CodeRelax.UseCases.DTOs.Requests.Authentication;
using UIT.CodeRelax.UseCases.DTOs.Responses.Authentication;
using UIT.CodeRelax.UseCases.DTOs.Responses.User;
using UIT.CodeRelax.UseCases.Repositories;
using UIT.CodeRelax.UseCases.Services.Impls;

namespace UIT.CodeRelax.Infrastructure.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly AppDbContext _dbContext;
        private readonly IMapper _mapper;

        public UserRepository(AppDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        async Task<SignUpRes> IUserRepository.SignUpAsync(SignUpReq signUpReq)
        {
            try
            {
                if (await _dbContext.Users.AnyAsync(u => u.Email == signUpReq.Email))
                {
                    //Console.Write("Erro from UserRepository: Email is existed.");
                    throw new Exception("Email is existed.");
                }


                var user = new User
                {
                    Email = signUpReq.Email,
                    Password = signUpReq.Password,
                    DisplayName = signUpReq.DisplayName,
                    CreatedAt = DateTime.UtcNow
                };

                await _dbContext.Users.AddAsync(user);
                await _dbContext.SaveChangesAsync();

                SignUpRes signUpRes = new SignUpRes();
                signUpReq.Email = user.Email;
                signUpReq.Password = user.Password;
                signUpReq.DisplayName = user.DisplayName;

                return signUpRes;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<UserProfileRes> GetUserById(int UserId)
        {
            try
            {
                var user = await _dbContext.Users.FirstOrDefaultAsync(u => u.Id == UserId);
                UserProfileRes userRes = new UserProfileRes();    
                if(user != null)
                {
                    userRes.Id = user.Id;
                    userRes.DisplayName = user.DisplayName;
                    userRes.Password = user.Password;
                    userRes.Email = user.Email;
                    userRes.Role = user.Role;
                    userRes.CreatedAt = user.CreatedAt;

                    return userRes;

                }
                return null ;

                //userRes.Google = "";
                //userRes.Github = "";
                //userRes.Facebook = "";

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        
    }
}
