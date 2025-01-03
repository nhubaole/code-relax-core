using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using UIT.CodeRelax.UseCases.DTOs.Responses;
using UIT.CodeRelax.UseCases.Services.Interfaces;

namespace UIT.CodeRelax.UseCases.Services.Impls
{
    public class StorageService: IStorageService
    {
        private readonly Supabase.Client _client;
        public StorageService(Supabase.Client client) { 
            _client = client;
           
        }

        public async Task<APIResponse<string>> Upload(IFormFile file, string bucketName)
        {
            try
            {

                using var memoryStream = new MemoryStream();

                await file.CopyToAsync(memoryStream);

                var lastIndexOfDot = file.FileName.LastIndexOf('.');
                string extension = file.FileName.Substring(lastIndexOfDot + 1);
                string updatedTime = DateTime.Now.ToString("yyyy-dd-MM-HH-mm-ss");
                string fileName = $"{bucketName}-{updatedTime}.{extension}";
                await _client.Storage.From(bucketName).Upload(
                    memoryStream.ToArray(),
                   fileName,
                    new Supabase.Storage.FileOptions
                    {
                        CacheControl = "3600",
                        Upsert = true

                    });
                var avatarUrl = _client.Storage.From(bucketName)
                                            .GetPublicUrl(fileName);
               

                //Uri uri = new Uri(avatarUrl);
                //string path = uri.AbsolutePath;

                //// Lấy phần cuối cùng của đường dẫn
                //string avatarFileName = Path.GetFileName(path);

                return new APIResponse<string>{                
                    StatusCode = StatusCodeRes.Success,
                    Message = "Upload image successful.",
                    Data = avatarUrl
                };
            }
            catch (Exception ex)
            {
                return new APIResponse<string>
                {
                    StatusCode = StatusCodeRes.InternalError,
                    Message = ex.Message,
                };
            }
        }
    }
}
