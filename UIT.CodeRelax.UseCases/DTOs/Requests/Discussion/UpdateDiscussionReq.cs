using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UIT.CodeRelax.UseCases.DTOs.Requests.Discussion
{
    public class UpdateDiscussionReq
    {
        public int ID { get; set; }
        public string Content { get; set; }
        public string Type { get; set; }
    }
}
