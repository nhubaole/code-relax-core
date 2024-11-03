﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UIT.CodeRelax.UseCases.DTOs.Responses.User
{
    public class UserProfileRes
    {
        public int Id { get; set; }

        public string Displayname { get; set; }

        public string Email { get; set; }

        public string Password { get; set; }

        public string Google { get; set; }
        public string Github { get; set; }
        public string Facebook { get; set; }

    }
}