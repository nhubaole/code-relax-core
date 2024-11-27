using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UIT.CodeRelax.Core.Entities;
using UIT.CodeRelax.UseCases.DTOs.Responses.Discussion;
using UIT.CodeRelax.UseCases.DTOs.Responses.Problem;
using UIT.CodeRelax.UseCases.DTOs.Responses.Rating;
using UIT.CodeRelax.UseCases.DTOs.Responses.Submission;
using UIT.CodeRelax.UseCases.DTOs.Responses.Testcase;
using UIT.CodeRelax.UseCases.DTOs.Responses.User;

namespace UIT.CodeRelax.UseCases.Mapper
{
    public class AppProfile : Profile
    {
        public AppProfile()
        {
            CreateMap<Problem, GetProblemRes>().ReverseMap();
            CreateMap<Testcase, TestcaseRes>().ReverseMap();
            CreateMap<Submission, GetSubmissionRes>().ReverseMap();
            CreateMap<User, UserProfileRes>().ReverseMap();
            CreateMap<Discussion, DiscussionRes>().ReverseMap();
            CreateMap<Rating, RatingRes>().ReverseMap();
        }
    }
}
