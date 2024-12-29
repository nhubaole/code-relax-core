using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UIT.CodeRelax.Core.Entities;
using UIT.CodeRelax.UseCases.DTOs.Responses;

namespace UIT.CodeRelax.UseCases.Services.Interfaces
{
    public interface ITagService
    {
        Task<APIResponse<IEnumerable<Tag>>> GetAllAsync();
    }
}
