using Microsoft.AspNetCore.Mvc;
using UIT.CodeRelax.UseCases.DTOs.Responses;

namespace UIT.CodeRelax.API.Controllers
{
    [ApiController]
    public abstract class ControllerExtensions : ControllerBase
    {



        protected IActionResult ApiOK<T>(APIResponse<T> obj)
        {
            if (obj.StatusCode == StatusCodeRes.Success)
            {
                return Ok(obj);
            }

            if (obj.StatusCode == StatusCodeRes.ReturnWithData)
            {
                return Created(string.Empty, obj);
            }

            if (obj.StatusCode == StatusCodeRes.ResourceNotFound || obj.StatusCode == StatusCodeRes.Deny)
            {
                return NoContent();
            }

            if (obj.StatusCode == StatusCodeRes.InvalidData || obj.StatusCode == StatusCodeRes.ResetContent)
            {
                return StatusCode(406, obj);
            }

            return StatusCode(500, obj);
        }

        //protected IActionResult ApiOK<T>(PagingResponse<T> obj)
        //{
        //    if (obj.StatusCode == CRUDStatusCodeRes.Success)
        //    {
        //        return Ok(obj);
        //    }

        //    if (obj.StatusCode == CRUDStatusCodeRes.ReturnWithData)
        //    {
        //        return Created(string.Empty, obj);
        //    }

        //    if (obj.StatusCode == CRUDStatusCodeRes.ResourceNotFound || obj.StatusCode == CRUDStatusCodeRes.Deny)
        //    {
        //        return NoContent();
        //    }

        //    if (obj.StatusCode == CRUDStatusCodeRes.InvalidData || obj.StatusCode == CRUDStatusCodeRes.ResetContent)
        //    {
        //        return StatusCode(406, obj.ErrorMessage);
        //    }

        //    return StatusCode(500, obj.ErrorMessage);
        //}

        //protected IActionResult CreateResponse<T>(CRUDResult<T> obj)
        //{
        //    if (obj.StatusCode == CRUDStatusCodeRes.Success)
        //    {
        //        return Ok(obj.Data);
        //    }

        //    if (obj.StatusCode == CRUDStatusCodeRes.ReturnWithData)
        //    {
        //        return Created(string.Empty, obj.Data);
        //    }

        //    if (obj.StatusCode == CRUDStatusCodeRes.ResourceNotFound || obj.StatusCode == CRUDStatusCodeRes.Deny)
        //    {
        //        return NoContent();
        //    }

        //    if (obj.StatusCode == CRUDStatusCodeRes.InvalidData || obj.StatusCode == CRUDStatusCodeRes.ResetContent)
        //    {
        //        return StatusCode(406, obj.ErrorMessage);
        //    }

        //    return StatusCode(500, obj.ErrorMessage);
        //}

        //[NonAction]
        //protected IActionResult CreateResponse<T>(PagingResponse<T> obj)
        //{
        //    if (obj.StatusCode == CRUDStatusCodeRes.Success)
        //    {
        //        return Ok(obj);
        //    }

        //    if (obj.StatusCode == CRUDStatusCodeRes.ResourceNotFound || obj.StatusCode == CRUDStatusCodeRes.Deny)
        //    {
        //        return NoContent();
        //    }

        //    if (obj.StatusCode == CRUDStatusCodeRes.InvalidData || obj.StatusCode == CRUDStatusCodeRes.ResetContent)
        //    {
        //        return StatusCode(406, obj.ErrorMessage);
        //    }

        //    return StatusCode(500, obj.ErrorMessage);
        //}

    }
}
