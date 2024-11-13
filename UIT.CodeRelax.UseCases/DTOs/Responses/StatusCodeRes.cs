using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UIT.CodeRelax.UseCases.DTOs.Responses
{


    public enum StatusCodeRes
    {
        Success,
        ResourceNotFound,
        Deny,
        NoExecute,
        Exists,
        InvalidAction,
        InvalidData,
        ReturnWithData,
        ResetContent,
        InternalError
    }

}
