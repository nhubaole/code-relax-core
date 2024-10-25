using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UIT.CodeRelax.Core.Entities;
using UIT.CodeRelax.UseCases.DTOs.Responses.Testcase;

namespace UIT.CodeRelax.UseCases.Repositories
{
    public interface ITestcaseRepository
    {
        Task<IEnumerable<TestcaseRes>> GetByProblemIDAsync(int problemID);
    }
}
