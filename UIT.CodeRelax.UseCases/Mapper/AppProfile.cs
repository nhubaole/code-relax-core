using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UIT.CodeRelax.Core.Entities;
using UIT.CodeRelax.UseCases.DTOs.Responses.Problem;
using UIT.CodeRelax.UseCases.DTOs.Responses.Submission;
using UIT.CodeRelax.UseCases.DTOs.Responses.Testcase;

namespace UIT.CodeRelax.UseCases.Mapper
{
    public class AppProfile : Profile
    {
        public AppProfile()
        {
            CreateMap<Problem, GetProblemRes>().ReverseMap();
            CreateMap<Testcase, TestcaseRes>().ReverseMap();
            CreateMap<Submission, GetSubmissionRes>().ReverseMap();
        }
    }
}
