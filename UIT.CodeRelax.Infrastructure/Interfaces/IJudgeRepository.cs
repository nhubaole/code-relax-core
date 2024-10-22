using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UIT.CodeRelax.Core.Entities;

namespace UIT.CodeRelax.Infrastructure.Interfaces
{
    public interface IJudgeRepository
    {
        public Problem GetAllProblems();
    }
}
