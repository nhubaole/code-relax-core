using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using UIT.CodeRelax.Core.Entities;
using UIT.CodeRelax.UseCases.DTOs.Requests.Authentication;
using UIT.CodeRelax.UseCases.DTOs.Responses.Discussion;
using UIT.CodeRelax.UseCases.Services.Impls;
using UIT.CodeRelax.UseCases.Services.Interfaces;

namespace UIT.CodeRelax.API.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class TagsController : ControllerExtensions
    {
        private readonly ITagService _tagService;
        public TagsController(ITagService tagService)
        {
            _tagService = tagService;
        }
        /// <summary>
        /// Get All
        /// </summary>
        /// <returns></returns>
        [ProducesResponseType(200, Type = typeof(IEnumerable<Tag>))]
        [HttpGet]

        public async Task<IActionResult> GetAll()
        {
            return ApiOK(await _tagService.GetAllAsync());
        }
    }
}
