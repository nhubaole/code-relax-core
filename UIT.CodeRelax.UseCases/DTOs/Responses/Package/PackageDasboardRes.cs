using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UIT.CodeRelax.UseCases.DTOs.Responses.Package
{
    public class PackageDasboardRes
    {
        public bool Success { get; set; }
        public int Id { get; set; } 
        public string Content { get; set; }
        public string UpdatedAgo { get; set; } = "No content";
        public IEnumerable<int> Levels { get; set; }  = new List<int>();
        public int NumberParticipants { get; set; } = 0;

    }
}
