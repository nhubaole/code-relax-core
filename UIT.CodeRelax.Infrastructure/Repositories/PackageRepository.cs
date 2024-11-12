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

        public async Task<List<ProblemPackage>> LoadProblemPackagesAsync(Package package)
        {
            try
            {
                if (package != null && package.ProblemPackages.Count == 0)
                {
                    return await _dbContext.ProblemPackages.
                        Include(p => p.PackageId == package.Id)
                        .ToListAsync();
                }
                return null;
            } catch (Exception ex) { throw;  }
        }
    }
}