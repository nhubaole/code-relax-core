using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UIT.CodeRelax.Core.Entities;
using UIT.CodeRelax.UseCases.DTOs.Requests;
using UIT.CodeRelax.UseCases.DTOs.Responses;
using UIT.CodeRelax.UseCases.DTOs.Responses.Judge;
using UIT.CodeRelax.UseCases.DTOs.Responses.Problem;
using UIT.CodeRelax.UseCases.DTOs.Responses.Testcase;
using UIT.CodeRelax.UseCases.Helper;
using UIT.CodeRelax.UseCases.Repositories;
using UIT.CodeRelax.UseCases.Services.Interfaces;

namespace UIT.CodeRelax.UseCases.Services.Impls
{
    public class ProblemService : IProblemService
    {
        private readonly IProblemRepository _problemRepository;
        private readonly ITestcaseRepository _testcaseRepository;
        public ProblemService(IProblemRepository problemRepository, ITestcaseRepository testcaseRepository)
        {
            _problemRepository = problemRepository;
            _testcaseRepository = testcaseRepository;
        }


        public async Task<APIResponse<SubmitCodeRes>> Submit(SubmitCodeReq req)
        {
            try
            {
                var testCases = await GetTestCase(req.ProblemId);
                var result = new SubmitCodeRes();
                bool isAccept = true;
                var outputs = new List<dynamic>();

                foreach (var testCase in testCases)
                {

                    var sourceFilePath = await GetSourceFilePath(req.Language, req.SourceCode, req.ProblemId, testCase.Input);

                    var response = await RunCode(sourceFilePath, req.Language);
                    if (response.Success is false)
                    {
                        isAccept = false;
                        return new APIResponse<SubmitCodeRes>
                        {
                            StatusCode = 400,
                            Message = "Compile Error",
                            Data = new SubmitCodeRes
                            {
                                Success = false,
                                Output = response.Errors
                            }
                        };
                    }

                    // Parse testCase.Output to compare
                    var expectedOutput = JsonConvert.DeserializeObject<Dictionary<string, object>>(testCase.Output)["output"];
                    if (!Comparer.CompareOutputs(response.Output, expectedOutput))
                    {
                        isAccept = false;
                        outputs.Add(response.Output);
                    }
                    else
                    {
                        outputs.Add(response.Output);
                    }

                    // Clean up source file after execution
                    File.Delete(sourceFilePath);
                }

                if (!isAccept)
                {
                    return new APIResponse<SubmitCodeRes>
                    {
                        StatusCode = 204,
                        Message = "One or more test case is failed",
                        Data = new SubmitCodeRes
                        {
                            Success = false,
                            Output = outputs[0] // Return the output that failed
                        }
                    };
                }

                return new APIResponse<SubmitCodeRes>
                {
                    StatusCode = 200,
                    Message = "All test case passed",
                    Data = new SubmitCodeRes
                    {
                        Success = true,
                        Output = outputs[0] // Return the first successful output
                    }
                };
            }
            catch (Exception ex)
            {
                return new APIResponse<SubmitCodeRes>
                {
                    StatusCode = 500,
                    Message = ex.Message
                };
            }
        }


        public async Task<IEnumerable<TestcaseRes>> GetTestCase(int problemID)
        {
            try
            {
                var testcase = await _testcaseRepository.GetByProblemIDAsync(problemID);

                return testcase;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private async Task<string> GetSourceFilePath(string language, string sourceCode, int problemId, string param)
        {
            string fullSourceCode = "";
            var tempPath = Path.GetTempFileName();
            var problem = await _problemRepository.GetByIDAsync(problemId);
            string functionName = Converter.ToPascalCase(problem.Title);

            var inputData = JsonConvert.DeserializeObject<JObject>(param);

            // Convert the parameters based on language
            string convertedParams = Converter.ConvertParamsForLanguage(language, inputData);

            string extension = language switch
            {
                "Python" => ".py",
                "C++" => ".cpp",
                "Java" => ".java",
                "javascript" => ".js",
                _ => throw new NotSupportedException("Unsupported language")
            };

            if (language == "Java")
            {
                fullSourceCode = $@"
                   import java.util.HashMap;
                   import java.util.Map;

                   public class Solution {{

                     public static void main(String[] args) {{
                         {convertedParams}  // Khởi tạo các tham số đầu vào từ JSON
                         System.out.println({functionName}({string.Join(", ", inputData.Properties().Select(param => param.Name))}));
                     }}

                     {sourceCode}
                   }}";

                var publicClassLine = fullSourceCode.Split('\n').FirstOrDefault(line => line.Contains("public class"));
                if (publicClassLine != null)
                {
                    //var className = publicClassLine.Split(' ')[2].Trim();  
                    tempPath = Path.Combine(Path.GetTempPath(), $"Solution.java");
                }
            }


            else if (language == "C++")
            {
                fullSourceCode = $@"#include <iostream>
                using namespace std;
                #include <vector>
                {sourceCode}

                // Hàm hỗ trợ in vector<int>
                void printVector(const vector<int>& vec) {{
                    cout << ""["";
                    for (size_t i = 0; i < vec.size(); ++i) {{
                        cout << vec[i];
                        if (i < vec.size() - 1)
                            cout << "", "";
                    }}
                    cout << ""]"";
                }}

                template<typename T>
                void printResult(T result) {{
                    cout << result << endl;
                }}

                // Nạp chồng hàm printResult cho vector<int>
                void printResult(const vector<int>& result) {{
                    printVector(result);
                    cout << endl;
                }}

                int main() {{
                    {convertedParams}  // Khởi tạo các tham số đầu vào từ JSON
                    auto result = {functionName}({string.Join(", ", inputData.Properties().Select(param => param.Name))}); 
                    printResult(result);  // In kết quả
                    return 0;
                }}";

                tempPath = Path.ChangeExtension(tempPath, extension);
            }



            else if (language == "Python")
            {
                fullSourceCode = $"{sourceCode}" +
                                 $"\nprint({functionName}({convertedParams}))";
            }
            else
            {
                tempPath = Path.ChangeExtension(tempPath, extension);
            }

            File.WriteAllText(tempPath, fullSourceCode);
            return tempPath;
        }

        private async Task<SubmitCodeRes> RunCode(string filePath, string language)
        {
            var processInfo = new ProcessStartInfo();
            processInfo.RedirectStandardOutput = true;
            processInfo.RedirectStandardError = true;
            processInfo.UseShellExecute = false;


            switch (language)
            {
                case "javascript":

                    processInfo.FileName = "node";
                    processInfo.Arguments = filePath;
                    break;
                case "Python":
                    processInfo.FileName = "python";
                    processInfo.Arguments = filePath;
                    break;

                case "C++":
                    var exeFilePath = Path.ChangeExtension(filePath, ".exe");
                    var compileProcessInfo = new ProcessStartInfo
                    {
                        FileName = "g++",
                        Arguments = $"-std=c++11 -o {exeFilePath} {filePath}",
                        RedirectStandardOutput = true,
                        RedirectStandardError = true,
                        UseShellExecute = false
                    };
                    using (var compileProcess = Process.Start(compileProcessInfo))
                    {
                        await compileProcess.WaitForExitAsync();
                        if (compileProcess.ExitCode != 0)
                        {
                            return new SubmitCodeRes
                            {
                                Success = false,
                                Output = "",
                                Errors = compileProcess.StandardError.ReadToEnd().Replace("\r\n", "")
                            };
                        }
                    }
                    processInfo.FileName = exeFilePath;
                    break;

                case "Java":
                    var compileJavaProcessInfo = new ProcessStartInfo
                    {
                        FileName = "javac",
                        Arguments = filePath,
                        RedirectStandardOutput = true,
                        RedirectStandardError = true,
                        UseShellExecute = false
                    };
                    using (var compileJavaProcess = Process.Start(compileJavaProcessInfo))
                    {
                        await compileJavaProcess.WaitForExitAsync();
                        if (compileJavaProcess.ExitCode != 0)
                        {
                            return new SubmitCodeRes
                            {
                                Success = false,
                                Output = "",
                                Errors = compileJavaProcess.StandardError.ReadToEnd().Replace("\r\n", "")
                            };
                        }
                    }
                    processInfo.FileName = "java";
                    processInfo.Arguments = Path.GetFileNameWithoutExtension(filePath);
                    processInfo.WorkingDirectory = Path.GetDirectoryName(filePath);
                    break;

                default:
                    return new SubmitCodeRes
                    {
                        Success = false,
                        Output = "",
                        Errors = "Unsupported language"
                    };
            }


            using var process = Process.Start(processInfo);
            var output = process.StandardOutput.ReadToEnd().Replace("\r\n", "");
            var errors = process.StandardError.ReadToEnd().Replace("\r\n", "");
            process.WaitForExit();

            return new SubmitCodeRes
            {
                Success = process.ExitCode == 0,
                Output = output,
                Errors = errors
            };
        }
    }
}
