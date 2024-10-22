using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UIT.CodeRelax.Core.Entities;
using UIT.CodeRelax.Infrastructure.Interfaces;
using UIT.CodeRelax.UseCases.DTOs.Responses;
using UIT.CodeRelax.UseCases.Services.Interfaces;

namespace UIT.CodeRelax.UseCases.Services.Impls
{
    public class JudgeService : IJudgeService
    {
        private readonly IJudgeRepository _repo;
        public JudgeService(IJudgeRepository repo)
        {
            _repo = repo;
        }

        public GetProblemRes GetProblems()
        {
            var res = _repo.GetAllProblems();
            GetProblemRes problemRes = new GetProblemRes
            {
                Id = res.Id,
                Title = res.Title,
                Difficulty = res.Difficulty,
                NumOfAcceptance = res.NumOfAcceptance,
                NumOfSubmission = res.NumOfSubmission,
            };
            return problemRes;
        }
    }
}
