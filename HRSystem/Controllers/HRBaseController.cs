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
    }
}

