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
using UIT.CodeRelax.UseCases.DTOs.Requests.User;
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

        async Task<SignUpRes> IUserRepository.AddUserAsync(SignUpReq signUpReq)
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
        public async Task<UserProfileRes> AuthorizeUser(LoginReq loginReq)
        {
            var user = await _dbContext.Users.FirstOrDefaultAsync(u => u.Email == loginReq.Email && u.Password == loginReq.Password);

            UserProfileRes userRes = new UserProfileRes();
            if (user != null)
            {
                userRes.Id = user.Id;
                userRes.DisplayName = user.DisplayName;
                userRes.Password = user.Password;
                userRes.Email = user.Email;
                userRes.Role = user.Role;
                userRes.CreatedAt = user.CreatedAt;

                return userRes;

            }
            return null;
        }

        public async Task<User> UpdateUserAsync(User user)
        {
            var userExisted = await _dbContext.Users.FirstOrDefaultAsync(u => u.Id == user.Id);

            if(userExisted != null)
            {
                userExisted.Email = user.Email;
                userExisted.Password = user.Password;
                userExisted.DisplayName = user.DisplayName;


                _dbContext.Users.Update(userExisted);

                var result = await _dbContext.SaveChangesAsync();

                if (result > 0)
                {
                    return userExisted;
                }
            }

            return null;

        }

        public async Task<IEnumerable<UserProfileRes>> GetAllUsersAsync()
        {
            try
            {
                var users = await _dbContext.Users
                            .Select(user => new UserProfileRes
                            {
                                Id = user.Id,
                                DisplayName = user.DisplayName,
                                Email = user.Email,
                                Role = user.Role,
                                CreatedAt = user.CreatedAt,
                            })
                            .ToListAsync();
                return users;

            }
            catch (Exception ex) {
                return Enumerable.Empty<UserProfileRes>();
            }
        }
    }
}
