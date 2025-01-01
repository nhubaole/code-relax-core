using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UIT.CodeRelax.UseCases.Helper
{
    public static class Generator
    {
        private static int _currentSerial = 0; 

        public static int GetNextSerial()
        {
            return ++_currentSerial; 
        }
    }
}
