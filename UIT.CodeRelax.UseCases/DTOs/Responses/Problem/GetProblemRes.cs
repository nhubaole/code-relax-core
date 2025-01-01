using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UIT.CodeRelax.Core.Entities;

namespace UIT.CodeRelax.UseCases.DTOs.Responses.Problem
{
    public class GetProblemRes
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string? Explaination { get; set; }
        public string FunctionName { get; set; }
        public string ReturnType { get; set; }
        public int Difficulty { get; set; }
        public int NumOfAcceptance { get; set; }
        public int NumOfSubmission { get; set; }
        public int TotalTestCase {  get; set; }
        public List<string> Tag { get; set; }
        public DateTime CreatedAt { get; set; }
        public double AverageRating { get; set; }
        public bool IsSolved { get; set; }
    }
}
