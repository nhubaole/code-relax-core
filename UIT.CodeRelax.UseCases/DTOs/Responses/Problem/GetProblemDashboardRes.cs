using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UIT.CodeRelax.UseCases.DTOs.Responses.Problem
{
    public class ProblemDashboardRes
    {
        public string Tittle {  get; set; }
        public bool IsSolved { get; set; }    
        public double Rating { get; set; }  
        public int Difficulty { get; set; }
        public double Acceptance { get; set; }

    }
}
