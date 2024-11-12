using UIT.CodeRelax.UseCases.DTOs.Requests.Problem;
using UIT.CodeRelax.UseCases.DTOs.Requests;
using UIT.CodeRelax.UseCases.DTOs.Responses.Judge;
using UIT.CodeRelax.UseCases.DTOs.Responses.Problem;
using UIT.CodeRelax.UseCases.DTOs.Responses.Testcase;
using UIT.CodeRelax.UseCases.DTOs.Responses;
using UIT.CodeRelax.Core.Entities;
using UIT.CodeRelax.UseCases.DTOs.Responses.Package;

namespace UIT.CodeRelax.UseCases.Services.Interfaces
{
    public interface IPackageService
    {
        Task<APIResponse<PackageDasboardRes>> CreateNewPackage(Package package);

        Task<APIResponse<IEnumerable<PackageDasboardRes>>> GetAllPackageDashboard();

    }
}
