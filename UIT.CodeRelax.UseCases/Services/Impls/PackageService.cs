using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;
using UIT.CodeRelax.Core.Entities;
using UIT.CodeRelax.UseCases.DTOs.Requests.Authentication;
using UIT.CodeRelax.UseCases.DTOs.Requests.Package;
using UIT.CodeRelax.UseCases.DTOs.Responses;
using UIT.CodeRelax.UseCases.DTOs.Responses.Authentication;
using UIT.CodeRelax.UseCases.DTOs.Responses.Package;
using UIT.CodeRelax.UseCases.Repositories;
using UIT.CodeRelax.UseCases.Services.Interfaces;

namespace UIT.CodeRelax.UseCases.Services.Impls
{
    public class PackageService : IPackageService
    {
        private readonly IPackageRepository _packageRepository;

        public PackageService(IPackageRepository packageRepository)
        {
            this._packageRepository = packageRepository;

        }
        string erroMesssage = string.Empty;
        public async Task<APIResponse<PackageDasboardRes>> CreateNewPackage(NewPackageReq package)
        {

            try
            {
                Package package1 = new Package() { Content = package.Content  };
                package1.Type = package.Type;

                var savedPackage = await _packageRepository.CreateNewPackage(package1);

                if (savedPackage != null)
                {
                    PackageDasboardRes res = new PackageDasboardRes();
                    res.Id = savedPackage.Id;
                    res.Content = savedPackage.Content;
                    res.UpdatedAt = savedPackage.UpdatedAt;
                    res.UpdateUpdatedAgo();



                    return new APIResponse<PackageDasboardRes>
                    {
                        StatusCode = StatusCodeRes.Success,
                        Data = res
                    };
                }

                return new APIResponse<PackageDasboardRes>
                {
                    StatusCode = StatusCodeRes.InvalidData,
                    Data = null
                    
                };

            } 
            catch (Exception ex)
            {
                return new APIResponse<PackageDasboardRes>
                {
                    StatusCode = StatusCodeRes.InvalidData,
                    Message = string.IsNullOrEmpty(ex.Message) ? "Not Success" : ex.Message,
                    Data = null
                };
            }
            finally
            {
                erroMesssage = string.Empty;
            }

        }

        public async Task<APIResponse<IEnumerable<PackageDasboardRes>>> GetAllPackageDashboard()
        {
            var packages = await _packageRepository.GetAllAsync();

            List<PackageDasboardRes> res = new List<PackageDasboardRes>();  
            foreach (var package in packages)
            {
                PackageDasboardRes pd = new PackageDasboardRes();
                pd.Id = package.Id;
                pd.Content = package.Content;
                pd.Levels = package.GetDifficulties();
                pd.NumberParticipants = 0;
                pd.UpdatedAt = package.UpdatedAt;

                pd.UpdateUpdatedAgo();


                res.Add(pd);
            }

            return new APIResponse<IEnumerable<PackageDasboardRes>>
            {
                StatusCode = StatusCodeRes.Success,
                Message = string.IsNullOrEmpty(erroMesssage) ? "Success" : erroMesssage,
                Data = res.ToArray()
            };
        }

        public async Task<APIResponse<PackageDasboardRes>> GetById(int id)
        {
            try
            {

                var package = await _packageRepository.GetByIDAsync(id);

                if (package != null)
                {
                    PackageDasboardRes res = new PackageDasboardRes();
                    res.Id = package.Id;
                    res.Content = package.Content;
                    res.UpdatedAt = package.UpdatedAt;
                    res.UpdateUpdatedAgo();



                    return new APIResponse<PackageDasboardRes>
                    {
                        StatusCode = StatusCodeRes.Success,
                        Data = res,
                    };
                }

                return new APIResponse<PackageDasboardRes>
                {
                    StatusCode = StatusCodeRes.InvalidData,
                    Data = null

                };

            }
            catch (Exception ex)
            {
                return new APIResponse<PackageDasboardRes>
                {
                    StatusCode = StatusCodeRes.InvalidData,
                    Message = string.IsNullOrEmpty(ex.Message) ? "Not Success" : ex.Message,
                    Data = null
                };
            }
            finally
            {
                erroMesssage = string.Empty;
            }
        }
    }
}