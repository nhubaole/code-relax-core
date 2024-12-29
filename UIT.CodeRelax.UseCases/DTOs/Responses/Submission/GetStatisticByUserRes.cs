using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UIT.CodeRelax.UseCases.DTOs.Responses.Submission
{
    public class GetStatisticByUserRes
    {
        public int EasyCount { get; set; }
        public int MediumCount { get; set; }
        public int HardCount { get; set; }
        public int NumOfSubmissions { get; set; }
        public float AcceptanceRate { get; set; }
    }
}
