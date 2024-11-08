
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UIT.CodeRelax.Core.Entities;


namespace UIT.CodeRelax.UseCases.DTOs.Requests.Problem
{
    public class CreateProblemReq
    {
        //Use to create new problem
        public int Level { get; set; }
        public string Title { get; set; } = String.Empty;
        public string? Explaination { get; set; }
        public int Difficulty { get; set; }
        public List<String> Tags { get; set; } = new List<String>();

    }
}
