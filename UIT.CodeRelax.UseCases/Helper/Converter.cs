using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Formats.Asn1;
using System.Globalization;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using UIT.CodeRelax.Core.Entities;
using UIT.CodeRelax.UseCases.DTOs.Responses.Testcase;

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


        public static string ConvertParamsForLanguage(string language, JObject inputData)
        {
            var paramList = new List<string>();

            foreach (var param in inputData)
            {
                var paramName = param.Key;
                var paramValue = param.Value;

                if (language == "C++")
                {
                    // Tạo mã khởi tạo cho C++ dựa trên kiểu của paramValue
                    if (paramValue is JArray array) // Mảng
                    {
                        var firstItem = array.First;
                        if (firstItem.Type == JTokenType.Integer)
                        {
                            var values = array.ToObject<int[]>();
                            paramList.Add($"vector<int> {paramName} = {{{string.Join(",", values)}}};");
                        }
                        else if (firstItem.Type == JTokenType.Float)
                        {
                            var values = array.ToObject<float[]>();
                            paramList.Add($"vector<float> {paramName} = {{{string.Join(",", values)}}};");
                        }
                        else if (firstItem.Type == JTokenType.String)
                        {
                            var values = array.ToObject<string[]>();
                            paramList.Add($"vector<string> {paramName} = {{{string.Join(", ", values.Select(v => $"\"{v}\""))}}};");
                        }
                    }
                    else if (paramValue.Type == JTokenType.Integer) // Số nguyên
                    {
                        paramList.Add($"int {paramName} = {paramValue};");
                    }
                    else if (paramValue.Type == JTokenType.Float) // Số thực
                    {
                        paramList.Add($"double {paramName} = {paramValue};");
                    }
                    else if (paramValue.Type == JTokenType.String) // Chuỗi
                    {
                        paramList.Add($"string {paramName} = \"{paramValue}\";");
                    }
                    else if (paramValue.Type == JTokenType.Boolean) // Boolean
                    {
                        paramList.Add($"bool {paramName} = {(paramValue.ToString().ToLower() == "true" ? "true" : "false")};");
                    }
                }
                else if (language == "Java")
                {
                    // Tạo mã khởi tạo cho Java dựa trên kiểu của paramValue
                    if (paramValue is JArray array) // Mảng
                    {
                        var firstItem = array.First;
                        if (firstItem.Type == JTokenType.Integer)
                        {
                            var values = array.ToObject<int[]>();
                            paramList.Add($"int[] {paramName} = new int[]{{{string.Join(",", values)}}};");
                        }
                        else if (firstItem.Type == JTokenType.Float)
                        {
                            var values = array.ToObject<float[]>();
                            paramList.Add($"float[] {paramName} = new float[]{{{string.Join(",", values)}}};");
                        }
                        else if (firstItem.Type == JTokenType.String)
                        {
                            var values = array.ToObject<string[]>();
                            paramList.Add($"String[] {paramName} = new String[]{{{string.Join(", ", values.Select(v => $"\"{v}\""))}}};");
                        }
                    }
                    else if (paramValue.Type == JTokenType.Integer) // Số nguyên
                    {
                        paramList.Add($"int {paramName} = {paramValue};");
                    }
                    else if (paramValue.Type == JTokenType.Float) // Số thực
                    {
                        paramList.Add($"double {paramName} = {paramValue};");
                    }
                    else if (paramValue.Type == JTokenType.String) // Chuỗi
                    {
                        paramList.Add($"String {paramName} = \"{paramValue}\";");
                    }
                    else if (paramValue.Type == JTokenType.Boolean) // Boolean
                    {
                        paramList.Add($"boolean {paramName} = {paramValue.ToString().ToLower()};");
                    }
                }
                else if (language == "Python")
                {
                    // Tạo mã khởi tạo cho Python dựa trên kiểu của paramValue
                    if (paramValue is JArray array) // Mảng
                    {
                        var firstItem = array.First;
                        if (firstItem.Type == JTokenType.Integer || firstItem.Type == JTokenType.Float)
                        {
                            var values = string.Join(", ", array);
                            paramList.Add($"{paramName} = [{values}]");
                        }
                        else if (firstItem.Type == JTokenType.String)
                        {
                            var values = string.Join(", ", array.Select(v => $"\"{v}\""));
                            paramList.Add($"{paramName} = [{values}]");
                        }
                    }
                    else if (paramValue.Type == JTokenType.Integer || paramValue.Type == JTokenType.Float) // Số nguyên hoặc số thực
                    {
                        paramList.Add($"{paramName} = {paramValue}");
                    }
                    else if (paramValue.Type == JTokenType.String) // Chuỗi
                    {
                        paramList.Add($"{paramName} = \"{paramValue}\"");
                    }
                    else if (paramValue.Type == JTokenType.Boolean) // Boolean
                    {
                        paramList.Add($"{paramName} = {paramValue.ToString().ToLower()}");
                    }
                }
            }

            return string.Join("\n", paramList); // Trả về chuỗi chứa các biến khởi tạo
        }

    }
}