using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UIT.CodeRelax.UseCases.DTOs.Responses.User
{
    public class GetLeaderBoardInfoRes
    {
        public string UserName { get; set; }
        public string UserAvatar { get; set; }
        public int Rank { get; set; }
        public List<UserRankInfo> ListUser { get; set; }

        public GetLeaderBoardInfoRes()
        {
            ListUser = new List<UserRankInfo>();
        }
    }

    public class UserRankInfo
    {
        public int Rank { get; set; }
        public string UserName { get; set; }
        public string UserAvatar { get; set; }
        public int TotalSubmission { get; set; }
        public int TotalSolved { get; set; }
        public decimal Acceptance { get; set; }
        public decimal Score { get; set; }
    }
}
