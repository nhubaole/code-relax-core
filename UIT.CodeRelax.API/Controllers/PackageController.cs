using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using UIT.CodeRelax.UseCases.Services.Interfaces;
using UIT.CodeRelax.UseCases.DTOs.Responses.Discussion;
using UIT.CodeRelax.UseCases.Services.Impls;
using UIT.CodeRelax.UseCases.DTOs.Responses.Package;
using UIT.CodeRelax.UseCases.DTOs.Requests.Discussion;
using UIT.CodeRelax.Core.Entities;
using UIT.CodeRelax.UseCases.DTOs.Requests.Package;
using Microsoft.AspNetCore.Authorization;

namespace UIT.CodeRelax.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    //[Authorize]

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
        public async Task<IActionResult> Create([FromBody] NewPackageReq req)
        {
            return ApiOK(await packageService.CreateNewPackage(req));
        }

        /// <summary>
        /// update
        /// </summary>
        /// <param name="id">ID của package cần cập nhật</param>
        /// <param name="req">Thông tin Pakacge mới</param>
        /// <returns></returns>
        [ProducesResponseType(200, Type = typeof(bool))]
        [HttpPut()]
        public async Task<IActionResult> Update(int id, [FromBody] NewPackageReq req)
        {
            return ApiOK(await packageService.UpdatePackage(id, req));
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

        /// <summary>
        /// Get problems of package by ID
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [ProducesResponseType(200, Type = typeof(bool))]
        [HttpGet("{id}/problems")]
        public async Task<IActionResult> GetProblemsOfPackage(int id)
        {
            return ApiOK(await packageService.GetProblemsOfPackage(id));
        }

        /// <summary>
        /// Add a problem to packge
        /// </summary>
        /// <param name="req">Object chứa idProblem và idPackge</param>
        /// <returns>List problems of pakage</returns>
        [ProducesResponseType(200, Type = typeof(bool))]
        [HttpPost("add-problem")]
        public async Task<IActionResult> AddProblemToPackge([FromBody] AddProblemPackageReq req)
        {
            return ApiOK(await packageService.AddProblemToPakage(req));
        }

    }
}
