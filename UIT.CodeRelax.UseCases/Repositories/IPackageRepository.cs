using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UIT.CodeRelax.Core.Entities;

namespace UIT.CodeRelax.UseCases.Repositories
{
    public interface IPackageRepository
    {
        public Task<IEnumerable<Package>> GetAllAsync();
        public Task<Package> GetByIDAsync(int id);
        public Task<Package> CreateNewPackage(Package package);
        public Task<IEnumerable<Problem>> LoadProblemsOfPackageAsync(int packageId);
        public Task<Package> UpdatePackageAsync (Package package);
        public Task DeletePackageAsync (Package package);

        public Task<IEnumerable<Problem>> AddProblemToPackage(int packageId, int problemId);

        public Task<IEnumerable<int>> GetLevelOfPackageAsync(IEnumerable<ProblemPackage> pps);
    }
}