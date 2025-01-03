﻿using AutoMapper;
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
using UIT.CodeRelax.UseCases.DTOs.Responses.Problem;
using UIT.CodeRelax.UseCases.Repositories;
using UIT.CodeRelax.UseCases.Services.Interfaces;

namespace UIT.CodeRelax.UseCases.Services.Impls
{
    public class PackageService : IPackageService
    {
        private readonly IPackageRepository _packageRepository;
        private readonly IMapper _mapper;
        public PackageService(IPackageRepository packageRepository, IMapper mapper)
        {
            this._packageRepository = packageRepository;
            this._mapper = mapper;

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
                    res.CalUpdatedAgo();

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
                pd.Levels = await _packageRepository.GetLevelOfPackageAsync(package.ProblemPackages);
                pd.NumberProblem = package.ProblemPackages.Count;
                pd.UpdatedAt = package.UpdatedAt;

                pd.CalUpdatedAgo();


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
                    res.Levels = await _packageRepository.GetLevelOfPackageAsync(package.ProblemPackages);
                    res.CalUpdatedAgo();

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

        public async Task<APIResponse<IEnumerable<GetProblemRes>>> GetProblemsOfPackage(int packageId)
        {
            try
            {
                var problems = await _packageRepository.LoadProblemsOfPackageAsync(packageId);
              
                List<GetProblemRes> res = new List<GetProblemRes>(); 
                foreach (Problem p in problems)
                {
                    res.Add(_mapper.Map<GetProblemRes>(p));
                }
                return new APIResponse<IEnumerable<GetProblemRes>>
                {
                    StatusCode = StatusCodeRes.Success,
                    Data = res               
                };
            }
            catch (Exception ex)
            {
                return new APIResponse<IEnumerable<GetProblemRes>>
                {
                    StatusCode = StatusCodeRes.InvalidData,
                    Message = $"Can't get problems of package {packageId}",
                };
            }
        }

        public async Task<APIResponse<PackageDasboardRes>> UpdatePackage(int packageId, NewPackageReq package)
        {
            try
            {
                Package existed  = await _packageRepository.GetByIDAsync(packageId);
                if(existed != null)
                {
                    existed.UpdatedAt = DateTime.UtcNow;
                    existed.Content = package.Content;
                    existed.Type = package.Type;

                    await _packageRepository.UpdatePackageAsync(existed);
                    PackageDasboardRes res = new PackageDasboardRes();
                    res.Id = existed.Id;
                    res.Content = existed.Content;
                    res.UpdatedAt = existed.UpdatedAt;
                    res.CalUpdatedAgo();
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

        public Task<APIResponse<string>> DeletePackage(int packgeId)
        {
            throw new NotImplementedException();
        }

        public async Task<APIResponse<IEnumerable<GetProblemRes>>> AddProblemToPakage(AddProblemPackageReq problemPackage)
        {
            try
            {
                var problems = await _packageRepository.AddProblemToPackage(problemPackage.PackageId, problemPackage.ProblemId);

                List<GetProblemRes> res = new List<GetProblemRes>();
                foreach (Problem p in problems)
                {
                    res.Add(_mapper.Map<GetProblemRes>(p));
                }
                return new APIResponse<IEnumerable<GetProblemRes>>
                {
                    StatusCode = StatusCodeRes.Success,
                    Data = res
                };
            }
            catch (Exception ex)
            {
                return new APIResponse<IEnumerable<GetProblemRes>>
                {
                    StatusCode = StatusCodeRes.ReturnWithData,
                    Message = ex.Message,
                };
            }
        }

        public string MapLevelToString (int level)
        {
            switch (level)
            {
                case 0:
                    return "easy";
                case 1:
                    return "medium";
                case 2:
                    return "hard";
                default:
                    return "hard"; 
            }
        }
    }
}