using AutoMapper;

namespace HRSystem.Controllers
{

    public class SalariesController : HRBaseController
    {
        readonly IConfiguration _configuration;
        readonly ADOProcedures _ADOProcedures;
        public SalariesController(IConfiguration configuration)
        {
            _configuration = configuration;
            string? connectionString = _configuration.GetConnectionString("Default");
            _ADOProcedures = new ADOProcedures(connectionString);
        }
        [HttpGet]
        public async Task<ActionResult<Pagination<SalaryObj>>> Get([FromQuery]SalariesParams P)
        {
            var Data = _ADOProcedures.GetSalaries(P);
            return Ok(new Pagination<SalaryObj>(P.PageIndex,P.PageSize, _ADOProcedures.SalariesCount, Data));
        }
    }
}
