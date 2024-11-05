using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UIT.CodeRelax.UseCases.DTOs.SubModels;

namespace UIT.CodeRelax.UseCases.DTOs.Responses.User
{
    public class UserHistoryPointRes
    {
        public int TotalPoint { get; set; }
        public List<HistoryPoint> HistoryPoints { get; set; }
   }
}
