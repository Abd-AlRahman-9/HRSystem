using HRDomain.Entities;
using HRDomain.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Net;

namespace HRSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HRBaseController : ControllerBase
    {
        //protected IActionResult HandleValidationErrors()
        //{
        //    var errors = ModelState.Values
        //        .SelectMany(v => v.Errors)
        //        .Select(e => e.ErrorMessage)
        //        .ToArray();

        //    var response = new
        //    {
        //        StatusCode = (int)HttpStatusCode.BadRequest,
        //        Message = "Validation errors occurred.",
        //        Errors = errors
        //    };

        //    var result = new ObjectResult(response)
        //    {
        //        StatusCode = (int)HttpStatusCode.BadRequest
        //    };

        //    result.ContentTypes.Clear(); // Clear any formatters

        //    return result;
        //}
    }
}

