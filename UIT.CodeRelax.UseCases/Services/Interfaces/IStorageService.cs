                        using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UIT.CodeRelax.UseCases.DTOs.Responses.Rating;
using UIT.CodeRelax.UseCases.DTOs.Responses;
using Microsoft.AspNetCore.Http;

namespace UIT.CodeRelax.UseCases.Services.Interfaces
{
    public interface IStorageService
    {
        public Task<APIResponse<string>> Upload(IFormFile file, string bucketName);
    }
}
