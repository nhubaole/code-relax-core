using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UIT.CodeRelax.UseCases.Helper
{
    internal class Comparer
    {
        public static bool CompareOutputs(dynamic actualOutput, dynamic expectedOutput)
        {
            JArray actualArray = null;
            JArray expectedArray = null;

            // Kiểm tra và chuyển actualOutput thành JArray nếu có dạng mảng
            if (actualOutput is JArray)
            {
                actualArray = actualOutput;
            }
            else if (expectedOutput is string && (expectedOutput == "true" || expectedOutput == "false"))
            {
                if (actualOutput == "0")
                {
                    actualOutput = "false";
                }
                else if (actualOutput == "1")
                {
                    actualOutput = "true";
                }
                else
                {
                    actualOutput = actualOutput.ToLowerInvariant();
                }
            }
            else if (actualOutput is IEnumerable<int> || actualOutput is IEnumerable<string>)
            {
                actualArray = new JArray(actualOutput);
            }
            else if (actualOutput is string actualString && actualString.StartsWith("[") && actualString.EndsWith("]"))
            {
                actualArray = JArray.Parse(actualString);
            }

            // Kiểm tra và chuyển expectedOutput thành JArray nếu có dạng mảng
            if (expectedOutput is JArray)
            {
                expectedArray = expectedOutput;
            }
            else if (expectedOutput is IEnumerable<int> || expectedOutput is IEnumerable<string>)
            {
                expectedArray = new JArray(expectedOutput);
            }
            else if (expectedOutput is string expectedString && expectedString.StartsWith("[") && expectedString.EndsWith("]"))
            {
                expectedArray = JArray.Parse(expectedString);
            }

            // So sánh hai JArray nếu cả hai đều là mảng
            if (actualArray != null && expectedArray != null)
            {
                return JToken.DeepEquals(actualArray, expectedArray);
            }

            // So sánh các kiểu dữ liệu đơn giản như số nguyên, chuỗi hoặc boolean trực tiếp
            if ((actualOutput is int || actualOutput is long || actualOutput is double || actualOutput is string || actualOutput is bool) &&
                (expectedOutput is int || expectedOutput is long || expectedOutput is double || expectedOutput is string || expectedOutput is bool))
            {
                return actualOutput == expectedOutput;
            }

            // Nếu không khớp kiểu hoặc khác cấu trúc, trả về false
            return false;
        }

    }
}
