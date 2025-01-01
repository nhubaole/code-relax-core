using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UIT.CodeRelax.UseCases.DTOs.Responses.User;

namespace UIT.CodeRelax.UseCases.DTOs.Responses.Discussion
{
    public class DiscussionRes
    {
        public int ID { get; set; }
        public UserProfileRes User { get; set; }
        public int ProblemID { get; set; }
        public string Content { get; set; }
        public string? ImageContent { get; set; }
        public string Type { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }

    }
}
