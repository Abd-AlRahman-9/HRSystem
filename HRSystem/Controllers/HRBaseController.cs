using HRDomain.Entities;
using HRDomain.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HRSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HRBaseController : ControllerBase
    {
        //public readonly IRepository<BaseTables> _tableRepo;
        //public HRBaseController(IRepository<BaseTables> tableRepo)
        //{
        //    _tableRepo = tableRepo;
        //}
    }
}
