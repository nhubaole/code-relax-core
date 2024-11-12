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

    }
}