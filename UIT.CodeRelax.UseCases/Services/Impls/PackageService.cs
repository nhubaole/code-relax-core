using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;
using UIT.CodeRelax.Core.Entities;
using UIT.CodeRelax.UseCases.DTOs.Requests.Authentication;
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
        public async Task<APIResponse<PackageDasboardRes>> CreateNewPackage(Package package)
        {

            try
            {
                if (IsValid(package))
                {
                    var savedPackage = await _packageRepository.CreateNewPackage(package);

                    if (savedPackage != null)
                    {
                        PackageDasboardRes res = new PackageDasboardRes();
                        res.Success = true;
                        res.Id = savedPackage.Id;
                        res.Content = savedPackage.Content;

                        int daysSinceUpdated = savedPackage.GetDaysSinceUpdated();
                        res.UpdatedAgo = $"Updated {daysSinceUpdated} day ago";
                        

                        return new APIResponse<PackageDasboardRes>
                        {
                            StatusCode = 200,
                            Data = res
                        };
                    }

                    return new APIResponse<PackageDasboardRes>
                    {
                        StatusCode = 400,
                        Message = string.IsNullOrEmpty(erroMesssage) ? "Not Success" : erroMesssage,
                        Data = new PackageDasboardRes
                        {
                            Success = false
                        }
                    };
                }
                else
                {
                    return new APIResponse<PackageDasboardRes>
                    {
                        StatusCode = 400,
                        Message = string.IsNullOrEmpty(erroMesssage) ? "Not Success" : erroMesssage,
                        Data = new PackageDasboardRes
                        {
                            Success = false
                        }
                    };
                }
            } catch (Exception ex)
            {
                return new APIResponse<PackageDasboardRes>
                {
                    StatusCode = 400,
                    Message = string.IsNullOrEmpty(ex.Message) ? "Not Success" : ex.Message,
                    Data = new PackageDasboardRes
                    {
                        Success = false
                    }
                };
            }
            finally
            {
                erroMesssage = string.Empty;
            }

        }
        public bool IsValid(Package package)
        {
            if (string.IsNullOrWhiteSpace(package.Type) || string.IsNullOrWhiteSpace(package.Content))
            {
                erroMesssage = "Check information of packge again";
                return false;
            }

            return true;
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


                res.Add(pd);
            }

            return new APIResponse<IEnumerable<PackageDasboardRes>>
            {
                StatusCode = 200,
                Message = string.IsNullOrEmpty(erroMesssage) ? "Success" : erroMesssage,
                Data = res.ToArray()
            };
        }
    }
}