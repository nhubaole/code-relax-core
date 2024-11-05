using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UIT.CodeRelax.UseCases.DTOs.SubModels
{
    public class HistoryPoint
    {
        public int NumStar {  get; set; }   
        public string Content { get; set; }
        public DateTime ReceivedAt { get; set; }
    }
}
