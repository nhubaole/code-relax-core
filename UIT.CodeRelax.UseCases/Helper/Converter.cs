using System;
using System.Collections.Generic;
using System.Formats.Asn1;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace UIT.CodeRelax.UseCases.Helper
{
    public class Converter
    {
        public static DateTime ConvertToDateTime(long utcDate)
        {
            var dateTimeInterval = new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc);
            dateTimeInterval.AddSeconds(utcDate).ToUniversalTime();

            return dateTimeInterval;
        }
        public static string ToPascalCase(string input)
        {
            TextInfo textInfo = new CultureInfo("en-US", false).TextInfo;
            string[] words = input.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
            for (int i = 0; i < words.Length; i++)
            {
                words[i] = textInfo.ToTitleCase(words[i]);
            }
            return string.Concat(words);
        }

        public static string DictionaryValuesToString(Dictionary<dynamic, dynamic> dictionary)
        {
            StringBuilder outputString = new StringBuilder();
            foreach (var pair in dictionary)
            {
                if (pair.Value is string)
                {
                    outputString.Append("\"").Append(pair.Value).Append("\", ");
                }
                else
                {
                    outputString.Append(pair.Value).Append(", ");
                }
            }
            if (outputString.Length > 0)
            {
                outputString.Length -= 2;
            }
            return outputString.ToString();
        }
    }


}
