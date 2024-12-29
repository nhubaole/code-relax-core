using AutoMapper;
using Microsoft.EntityFrameworkCore;
using UIT.CodeRelax.Core.Entities;
using UIT.CodeRelax.Infrastructure.DataAccess;
using UIT.CodeRelax.Infrastructure.Repositories;

namespace UIT.CodeRelax.UseCases.Repositories
{
    public class PackageRepository : IPackageRepository
    {
        private readonly AppDbContext _dbContext;
        private readonly IMapper _mapper;

        public PackageRepository(AppDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
            _mapper = mapper;
        }

        public async Task AddProblemToPackage(int packageId, Problem problem)
        {
            var package = await GetByIDAsync(packageId);
            if (package == null) {
                throw new InvalidOperationException($"Package with ID {packageId} does not exist.");
            }

            var problemPackage = new ProblemPackage
            {
                PackageId = packageId,
                Problem = problem
            };

            package.ProblemPackages.Add(problemPackage);

            await _dbContext.SaveChangesAsync();
        }

        public async Task<Package> CreateNewPackage(Package package)
        {
            try
            {
                package.CreatedAt = DateTime.UtcNow;
                package.UpdatedAt = DateTime.UtcNow;

                await _dbContext.Packages.AddAsync(package);
                await _dbContext.SaveChangesAsync();

                var savedPackage  = await _dbContext.Packages.FirstOrDefaultAsync( p => p.Id == package.Id);
                if(savedPackage != null)
                {
                    return savedPackage;
                } 
                else
                {
                    throw new Exception("\"Package was not saved successfully.");
                }

            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task DeletePackageAsync(Package package)
        {
            var existedPackage = await GetByIDAsync(package.Id);
            if (existedPackage != null)
            {
                _dbContext.Set<Package>().Remove(package);
                await _dbContext.SaveChangesAsync();
            }
        }

        public async Task<IEnumerable<Package>> GetAllAsync()
        {
            try
            {
                return await _dbContext.Packages
                                .Include(p => p.ProblemPackages)
                                .ToListAsync();
            }
            catch (Exception ex)
            {
                throw ;
            }
        }

        public async Task<Package> GetByIDAsync(int id)
        {
            try
            {
                return await _dbContext.Packages.FirstOrDefaultAsync(p => p.Id == id);
            }
            catch (Exception ex)
            {
                throw ;
            }
        }

        public async Task<Package> UpdatePackageAsync(Package package)
        {
            try
            {
                package.UpdatedAt = DateTime.UtcNow;

                var savedPackage = await _dbContext.Packages.FirstOrDefaultAsync(p => p.Id == package.Id);
                if (savedPackage != null)
                {

                    _dbContext.Packages.Update(package);
                    await _dbContext.SaveChangesAsync();

                    return savedPackage;
                }
                else
                {
                    throw new Exception("\"Package was not saved successfully.");
                }

            }
            catch (Exception ex)
            {
                throw;
            }
        }

        async Task<IEnumerable<Problem>> IPackageRepository.LoadProblemsOfPackageAsync(int packageId)
        {
            try
            {
                return await _dbContext.Packages
                   .Where(p => p.Id == packageId)
                   .SelectMany(p => p.ProblemPackages)
                   .Select(pp => pp.Problem)
                   .ToListAsync();
            }
            catch (Exception ex) { throw; }
        }
    }
}