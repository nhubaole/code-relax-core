using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UIT.CodeRelax.UseCases.DTOs.Responses.Testcase
{
    public class TestcaseRes
    {
        public int ID { get; set; }
        public string Input { get; set; }
        public string Output { get; set; }
        public bool IsExample { get; set; }

    }
}
