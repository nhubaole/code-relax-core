using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using UIT.CodeRelax.Core.Entities;
using UIT.CodeRelax.UseCases.DTOs.Requests;
using UIT.CodeRelax.UseCases.DTOs.Requests.Authentication;
using UIT.CodeRelax.UseCases.DTOs.Requests.Problem;
using UIT.CodeRelax.UseCases.DTOs.Responses;
using UIT.CodeRelax.UseCases.DTOs.Responses.Authentication;
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
        private string errorMessage = string.Empty;
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

                foreach (var testCase in testCases.Data)
                {

                    var sourceFilePath = await GetSourceFilePath(req.Language, req.SourceCode, req.ProblemId, testCase.Input);

                    var response = await RunCode(sourceFilePath, req.Language);
                    if (response.Success is false)
                    {
                        isAccept = false;
                        return new APIResponse<SubmitCodeRes>
                        {
                            StatusCode = StatusCodeRes.InvalidData,
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
                        StatusCode = StatusCodeRes.Deny,
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
                    StatusCode = StatusCodeRes.Success,
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
                    StatusCode = StatusCodeRes.InternalError,
                    Message = ex.Message
                };
            }
        }


        public async Task<APIResponse<IEnumerable<TestcaseRes>>> GetTestCase(int problemID)
        {
            try
            {
                var testcase = await _testcaseRepository.GetByProblemIDAsync(problemID);

                return new APIResponse<IEnumerable<TestcaseRes>>
                {
                    StatusCode = StatusCodeRes.Success,
                    Message = "Success",
                    Data = testcase
                };
            }
            catch (Exception ex)
            {
                return new APIResponse<IEnumerable<TestcaseRes>>
                {
                    StatusCode = StatusCodeRes.InternalError,
                    Message = ex.Message,
                    Data = null
                };
            }
        }

        private async Task<string> GetSourceFilePath(string language, string sourceCode, int problemId, string param)
        {
            string fullSourceCode = "";
            var tempPath = Path.GetTempFileName();
            var problem = await _problemRepository.GetByIDAsync(problemId);
            string functionName = problem.FunctionName;

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
                    import java.util.Arrays;  // Import Arrays for array printing

                    public class Solution {{

                      public static void main(String[] args) {{
                          {convertedParams}  // Initialize input parameters from JSON

                          // Print the result with Arrays.toString if needed
                          System.out.println(
                              {functionName}({string.Join(", ", inputData.Properties().Select(param => param.Name))}) instanceof int[] 
                                  ? Arrays.toString({functionName}({string.Join(", ", inputData.Properties().Select(param => param.Name))})) 
                                  : {functionName}({string.Join(", ", inputData.Properties().Select(param => param.Name))})
                          );
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
                string[] lines = convertedParams.Split('\n');
                string numsValue = lines[0].Split('=')[1].Trim();
                string targetValue = lines[1].Split('=')[1].Trim();

                // Format the params as function arguments
                string formattedParams = $"{numsValue}, {targetValue}";
                fullSourceCode = $"{sourceCode}" +
                                 $"\nprint({functionName}({formattedParams}))";
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

        public async Task<APIResponse<GetProblemRes>> GetByID(int problemID)
        {
            try
            {
                var problem = await _problemRepository.GetByIDAsync(problemID);
                return new APIResponse<GetProblemRes>
                {
                    StatusCode = StatusCodeRes.Success,
                    Message = "Success",
                    Data = problem
                };
            }

            catch (Exception ex)
            {
                return new APIResponse<GetProblemRes>
                {
                    StatusCode = StatusCodeRes.InternalError,
                    Message = ex.Message,
                    Data = null
                };
            }
        }



        public async Task<APIResponse<GetProblemRes>> CreateNewProblem(CreateProblemReq req)
        {
            try
            {
                Problem problem = new Problem()
                {
                    Title = req.Title,
                    Explaination = req.Explaination,
                    Difficulty = req.Difficulty,
                };

                Problem newPro = await _problemRepository.CreateNewProblem(problem, req.Tags);

                return new APIResponse<GetProblemRes>
                {
                    StatusCode = StatusCodeRes.Success,
                    Message = "Success",
                    Data = new GetProblemRes()
                    {
                        Id = newPro.Id,
                        Title = newPro.Title,
                        Explaination = newPro.Explaination,
                        Difficulty = newPro.Difficulty,
                        NumOfAcceptance = 0,
                        NumOfSubmission = 0,
                        CreatedAt = newPro.CreatedAt,

                    },
                };
            }
            catch (Exception ex)
            {
                return new APIResponse<GetProblemRes>
                {
                    StatusCode = StatusCodeRes.InternalError,
                    Message = ex.Message,
                    Data = null,
                };
                throw new Exception("ProblemService: An error occurred while creating problem\n", ex);
            }
            finally { errorMessage = String.Empty; }
        }

        public async Task<APIResponse<SubmitCodeRes>> RunCode(SubmitCodeReq req)
        {
            try
            {
                var testCases = await GetTestCase(req.ProblemId);
                var exampleTestCases = testCases.Data.Where(x => x.IsExample).ToList();
                var result = new SubmitCodeRes();
                bool isAccept = true;
                var outputs = new List<dynamic>();

                foreach (var testCase in exampleTestCases)
                {

                    var sourceFilePath = await GetSourceFilePath(req.Language, req.SourceCode, req.ProblemId, testCase.Input);

                    var response = await RunCode(sourceFilePath, req.Language);
                    if (response.Success is false)
                    {
                        isAccept = false;
                        return new APIResponse<SubmitCodeRes>
                        {
                            StatusCode = StatusCodeRes.InvalidData,
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
                        StatusCode = StatusCodeRes.Deny,
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
                    StatusCode = StatusCodeRes.Success,
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
                    StatusCode = StatusCodeRes.InternalError,
                    Message = ex.Message
                };
            }
        }

        public async Task<APIResponse<IEnumerable<GetProblemRes>>> GetAll()
        {
            try
            {
                var problems = await _problemRepository.GetAllAsync();
                if (problems == null || !problems.Any())
                {
                    return new APIResponse<IEnumerable<GetProblemRes>>
                    {
                        StatusCode = StatusCodeRes.ResourceNotFound,
                        Message = "No data",
                        Data = problems
                    };
                }
                return new APIResponse<IEnumerable<GetProblemRes>>
                {
                    StatusCode = StatusCodeRes.Success,
                    Message = "Success",
                    Data = problems
                };
            }
            catch (Exception ex)
            {
                return new APIResponse<IEnumerable<GetProblemRes>>
                {
                    StatusCode = StatusCodeRes.InternalError,
                    Message = ex.Message
                };
            }
        }

        public async Task<APIResponse<bool>> Delete(int id)
        {
            try
            {
                var isDeleted = await _problemRepository.DeleteAsync(id);

                return new APIResponse<bool>
                {
                    StatusCode = StatusCodeRes.Success,
                    Message = "Success",
                    Data = isDeleted
                };
            }
            catch (Exception ex)
            {
                return new APIResponse<bool>
                {
                    StatusCode = StatusCodeRes.InternalError,
                    Message = ex.Message,
                    Data = false
                };
            }
        }

        public async Task<APIResponse<bool>> Update(UpdateProblemReq req, int id)
        {
            try
            {
                var isUpdated = await _problemRepository.UpdateAsync(req, id);

                return new APIResponse<bool>
                {
                    StatusCode = StatusCodeRes.ReturnWithData,
                    Message = "Success",
                    Data = isUpdated
                };
            }
            catch (Exception ex)
            {
                return new APIResponse<bool>
                {
                    StatusCode = StatusCodeRes.InternalError,
                    Message = ex.Message,
                    Data = false
                };
            }
        }
    }
}
