using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UIT.CodeRelax.UseCases.DTOs.Requests.User
{
    public class UserProfileReq
    {
        //TODDO: check
        public int Id { get; set; }
        public string DisplayName { get; set; }

        public string Email { get; set; }

        public string Password { get; set; }

        public int Role { get; set; }

        //public string Google { get; set; }
        //public string Github { get; set; }
        //public string Facebook { get; set; }


    }
}
