using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UIT.CodeRelax.UseCases.DTOs.Responses
{
    public class GetProblemRes
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string? Explaination { get; set; }
        public int Difficulty { get; set; }
        public int NumOfAcceptance { get; set; }
        public int NumOfSubmission { get; set; }
        public DateTime CreatedAt { get; set; }
        public List<TestcaseDto>? Testcases { get; set; } = new List<TestcaseDto>();
    }

    public class TestcaseDto
    {
        public int Id { get; set; }
        public string Input { get; set; }
        public string Output { get; set; }
    }
}
