using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UIT.CodeRelax.Core.Entities;
using UIT.CodeRelax.UseCases.DTOs.Responses.Discussion;
using UIT.CodeRelax.UseCases.DTOs.Responses.Testcase;

namespace UIT.CodeRelax.UseCases.DTOs.Responses.Problem
{
    public class ProblemDetailRes
    {
        //TODO check lai example, discussion, ratingsg
        public int Level { get; set; }  
        public string Title { get; set; }
        public bool IsSolved { get; set; }  
        public List<Tag> Tags { get; set; } 
        public int UserRating { get; set; }
        public string Description { get; set; } 
        public List<TestcaseRes> Examples { get; set; }    

        public double Accepted { get; set; }  
        public double Submissions { get; set; }  
        public double AcceptanceRate { get; set; }

        public List<DiscussionRes> Discussions { get; set; }
        public List <Rating> Ratings { get; set; }

    }
}
