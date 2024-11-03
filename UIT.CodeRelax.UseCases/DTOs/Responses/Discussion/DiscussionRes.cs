using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UIT.CodeRelax.UseCases.DTOs.Responses.Discussion
{
    public class DiscussionRes
    {
        public string UserDisplayName { get; set; }
        public string Content { get; set;}

        public string UserImage { get; set; }

        public DateTime CreatedAt { get; set; }

        public int TotalRatings { get; set; }
        public int NumberRating5 { get; set; }
        public int NumberRating4 { get; set; }
        public int NumberRating3 { get; set; }
        public int NumberRating2 { get; set; }
        public int NumberRating1 { get; set; }



    }
}
