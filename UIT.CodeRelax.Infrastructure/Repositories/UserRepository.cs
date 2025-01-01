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
                signUpRes.Email = user.Email;
                signUpRes.DisplayName = user.DisplayName;

                return signUpRes;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<UserProfileRes> GetUserByIdAsync(int UserId)
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
                    userRes.AvatarUrl = user.AvatarUrl;
                    userRes.Facebook = user.Facebook;
                    userRes.Google = user.Google;
                    userRes.Github = user.Github;
                    return userRes;

                }
                return null;

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
                userExisted.AvatarUrl = user.AvatarUrl;
                userExisted.Role = user.Role;   
                userExisted.CreatedAt = user.CreatedAt;
                userExisted.Facebook = user.Facebook;
                userExisted.Google = user.Google;
                userExisted.Github  = user.Github;

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
                                AvatarUrl = user.AvatarUrl,
                                Github = user.Github,
                                Google = user.Google,
                                Facebook = user.Facebook,
                            })
                            .ToListAsync();
                return users;

            }
            catch (Exception ex) {
                return Enumerable.Empty<UserProfileRes>();
            }
        }

        public async Task<GetLeaderBoardInfoRes> GetLeaderBoardInfoAsync(int userId)
        {
            try
            {
                var users = await _dbContext.Users
                                    .Include(u => u.Submissions)
                                        .ThenInclude(s => s.Problem)
                                    .ToListAsync();

                var leaderboard = new List<UserRankInfo>();

                foreach (var user in users)
                {
                    var submissions = user.Submissions;

                    int easySolved = 0;
                    int mediumSolved = 0;
                    int hardSolved = 0;

                    foreach (var submission in submissions)
                    {
                        if (submission.Status == 1)
                        {
                            if (submission.Problem.Difficulty == 0)
                            {
                                easySolved++;
                            }
                            else if (submission.Problem.Difficulty == 1)
                            {
                                mediumSolved++;
                            }
                            else if (submission.Problem.Difficulty == 2)
                            {
                                hardSolved++;
                            }
                        }
                    }

                    int totalSubmissions = submissions.Count;
                    if (totalSubmissions == 0)
                    {
                        continue;
                    }

                    decimal score = (easySolved + mediumSolved * 2 + hardSolved * 3) / (decimal)totalSubmissions;

                    leaderboard.Add(new UserRankInfo
                    {
                        UserName = user.DisplayName,
                        UserAvatar = user.AvatarUrl,
                        Rank = 0, 
                        TotalSubmission = totalSubmissions,
                        TotalSolved = easySolved + mediumSolved + hardSolved,
                        Acceptance = Math.Round((decimal)(easySolved + mediumSolved + hardSolved) / totalSubmissions * 100, 2),
                        Score = score
                    });
                }

                leaderboard = leaderboard.OrderByDescending(x => x.Score).ToList();

                int rank = 1;
                foreach (var userRank in leaderboard)
                {
                    userRank.Rank = rank++;
                }

                var currentUser = leaderboard.FirstOrDefault(x => x.UserName == users.FirstOrDefault(u => u.Id == userId)?.DisplayName);

                return new GetLeaderBoardInfoRes
                {
                    UserName = currentUser?.UserName ?? "",
                    UserAvatar = currentUser?.UserAvatar ?? "",
                    Rank = currentUser?.Rank ?? 0,
                    ListUser = leaderboard
                };
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public async Task<UserProfileRes> GetUserByEmailAsync(string email)
        {
            try
            {
                var user = await _dbContext.Users.FirstOrDefaultAsync(u => u.Email == email);
                UserProfileRes userRes = new UserProfileRes();
                if (user != null)
                {
                    userRes.Id = user.Id;
                    userRes.DisplayName = user.DisplayName;
                    userRes.Password = user.Password;
                    userRes.Email = user.Email;
                    userRes.Role = user.Role;
                    userRes.CreatedAt = user.CreatedAt;
                    userRes.AvatarUrl = user.AvatarUrl;
                    userRes.Google = user.Google;
                    userRes.Facebook = user.Facebook;
                    userRes.Github = user.Github;

                    return userRes;

                }
                return null;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
