using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UIT.CodeRelax.UseCases.DTOs.Responses;

namespace UIT.CodeRelax.UseCases.Services.Interfaces
{
    public interface IJudgeService
    {
        public GetProblemRes GetProblems();
    }
}
