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
                    if (response.Output != testCase.Output)
                    {
                        isAccept = false;
                        outputs.Add(response.Output);
                    }
                    outputs.Add(response.Output);
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
                            Output = outputs[0]
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
                        Output = outputs[0]
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
                fullSourceCode = @$"public class Solution{{

	                                public static void main(String[] args){{

		                                System.out.println({functionName}({param}));
	                                }}
                                    {sourceCode}
                                }}";
                var publicClassLine = fullSourceCode.Split('\n').FirstOrDefault(line => line.Contains("public class"));
                if (publicClassLine != null)
                {
                    var className = publicClassLine.Split(' ')[2];
                    tempPath = Path.Combine(Path.GetTempPath(), $"Solution{extension}");
                }
            }
            else if (language == "C++")
            {
                fullSourceCode = $@"#include <iostream>
                                using namespace std;
                                {sourceCode}
                                int main() {{
                                  
                                    cout << {functionName}({param}) << endl;

                                    return 0;
                                }}";

                tempPath = Path.ChangeExtension(tempPath, extension);
            }
            else if (language == "Python")
            {
                fullSourceCode = $"{sourceCode}" +
                    $"\nprint({functionName}({param}))";
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
                        Arguments = $"-o {exeFilePath} {filePath}",
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
