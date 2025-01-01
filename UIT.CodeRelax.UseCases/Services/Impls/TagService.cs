using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using UIT.CodeRelax.Core.Entities;
using UIT.CodeRelax.UseCases.DTOs.Requests.Article;
using UIT.CodeRelax.UseCases.DTOs.Responses;
using UIT.CodeRelax.UseCases.DTOs.Responses.Articles;
using UIT.CodeRelax.UseCases.DTOs.Responses.Quiz;
using UIT.CodeRelax.UseCases.Helper;
using UIT.CodeRelax.UseCases.Repositories;
using UIT.CodeRelax.UseCases.Services.Interfaces;

namespace UIT.CodeRelax.UseCases.Services.Impls
{
    public class TagService : ITagService
    {
        private readonly ITagRespository _tagRepository;

        public TagService(ITagRespository tagRepository)
        {
            _tagRepository = tagRepository;
        }

        public async Task<APIResponse<IEnumerable<Tag>>> GetAllAsync()
        {
            try
            {
                var tag = await _tagRepository.GetAllAsync();

                return new APIResponse<IEnumerable<Tag>>
                {
                    StatusCode = StatusCodeRes.Success,
                    Message = "Success",
                    Data = tag
                };
            }
            catch (Exception ex)
            {
                return new APIResponse<IEnumerable<Tag>>
                {
                    StatusCode = StatusCodeRes.InternalError,
                    Message = ex.Message,
                    Data = null
                };
            }

        }
    }


}
