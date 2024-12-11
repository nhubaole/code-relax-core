using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using UIT.CodeRelax.UseCases.Services.Interfaces;
using UIT.CodeRelax.UseCases.DTOs.Responses.Discussion;
using UIT.CodeRelax.UseCases.Services.Impls;
using UIT.CodeRelax.UseCases.DTOs.Responses.Package;
using UIT.CodeRelax.UseCases.DTOs.Requests.Discussion;
using UIT.CodeRelax.Core.Entities;
using UIT.CodeRelax.UseCases.DTOs.Requests.Package;

namespace UIT.CodeRelax.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PackageController : ControllerExtensions
    {
        private readonly IPackageService packageService;

        public PackageController(IPackageService packageService)
        {
            this.packageService = packageService;
        }

        /// <summary>
        /// Get All Package
        /// </summary>
        /// <returns></returns>
        [ProducesResponseType(200, Type = typeof(IEnumerable<PackageDasboardRes>))]
        [HttpGet()]
        public async Task<IActionResult> GetAllPackageDashboard()
        {
            var result = await packageService.GetAllPackageDashboard();
            return ApiOK(result);
        }


        /// <summary>
        /// Create
        /// </summary>
        /// <param name="req"></param>
        /// <returns></returns>
        [ProducesResponseType(200, Type = typeof(bool))]
        [HttpPost()]
        public async Task<IActionResult> Create(NewPackageReq req)
        {
            return ApiOK(await packageService.CreateNewPackage(req));
        }

        /// <summary>
        /// Get package by ID
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [ProducesResponseType(200, Type = typeof(bool))]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            return ApiOK(await packageService.GetById(id));
        }
    }
}
