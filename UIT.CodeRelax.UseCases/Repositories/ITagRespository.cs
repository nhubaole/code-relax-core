using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UIT.CodeRelax.Core.Entities;

namespace UIT.CodeRelax.UseCases.Repositories
{
    public interface ITagRespository
    {
        public Task<Tag> CreateNewTag(string name);
        public Task<IEnumerable<Tag>> GetAllAsync();

        public Task<int> GetIdByName(string name); 

    }
}
