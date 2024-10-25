using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UIT.CodeRelax.UseCases.DTOs.Responses.Judge
{
    public class SubmitCodeRes
    {
        public bool Success { get; set; }
        public string Output { get; set; }
        public string Errors { get; set; }
    }
}
