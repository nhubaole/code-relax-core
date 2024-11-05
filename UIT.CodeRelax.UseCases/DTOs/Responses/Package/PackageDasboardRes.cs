using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UIT.CodeRelax.UseCases.DTOs.Responses.Package
{
    public class PackageDasboardRes
    {
        public string Title { get; set; }   
        public string UpdatedAgo { get; set; }
        public List<int> Levels { get; set; }   
        public int NumberParticipants { get; set; } 

    }
}
