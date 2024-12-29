
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Nodes;
using System.Threading.Tasks;
using UIT.CodeRelax.Core.Entities;


namespace UIT.CodeRelax.UseCases.DTOs.Requests.Problem
{
    public class CreateProblemReq
    {
        public string Title { get; set; } = String.Empty;
        public string? Explaination { get; set; }
        public int Difficulty { get; set; }

        public string? FunctionName { get; set; } = String.Empty;
        public string? ReturnType { get; set; } = String.Empty;
        public List<string> Tags { get; set; }
        public List<string> Input { get; set; }
        public List<string> Output { get; set; }

    }
}
