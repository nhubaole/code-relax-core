using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UIT.CodeRelax.Core.Entities;
using UIT.CodeRelax.Infrastructure.DataAccess;
using UIT.CodeRelax.UseCases.Repositories;

namespace UIT.CodeRelax.Infrastructure.Repositories
{
    public class TagRepository : ITagRespository
    {
        private readonly AppDbContext _dbContext;
        public TagRepository(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Tag> CreateNewTag(string name)
        {
            try
            {
                Tag tag = new Tag
                {
                    Name = name
                };

                await _dbContext.Tags.AddAsync(tag);
                await _dbContext.SaveChangesAsync(); 

                return tag;
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while creating the tag.", ex);
            }
        }

        public async Task<IEnumerable<Tag>> GetAllAsync()
        {
            var tag = await _dbContext.Tags.ToListAsync();
            return tag;
        } 

        public async Task<int> GetIdByName(string name)
        {
            try
            {
                var existingTag = await _dbContext.Tags.FirstOrDefaultAsync(t => t.Name == name);
                if (existingTag != null)
                {

                    return existingTag.Id;

                }
                return -1;
            }
            catch (Exception ex){
                throw ex;
            }
        }
    }
}
