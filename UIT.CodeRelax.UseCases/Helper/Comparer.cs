using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UIT.CodeRelax.UseCases.Helper
{
    internal class Comparer
    {
        private static bool CompareOutputs(dynamic actual, dynamic expected)
        {
            // Compare if both are dictionaries (for JSON objects)
            if (actual is IDictionary<string, dynamic> && expected is IDictionary<string, dynamic>)
            {
                return actual.Count == expected.Count && !actual.Except(expected).Any();
            }

            // If both are arrays or lists, compare element by element
            if (actual is IEnumerable<dynamic> && expected is IEnumerable<dynamic>)
            {
                return actual.SequenceEqual(expected);
            }

            // For primitive types (int, string, etc.), do a direct comparison
            return actual == expected;
        }


    }
}
