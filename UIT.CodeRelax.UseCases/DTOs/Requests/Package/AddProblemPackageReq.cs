using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UIT.CodeRelax.UseCases.DTOs.Requests.Package
{
    public class AddProblemPackageReq
    {
        public int ProblemId { get; set; }
        public int PackageId { get; set; }  
    }
}
