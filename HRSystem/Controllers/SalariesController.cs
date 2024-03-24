using AutoMapper;
using HRDomain.Entities.DrivenEntities;
using HRDomain.Specification.EntitiesSpecification;
using HRDomain.Specification.Params;

namespace HRSystem.Controllers
{

    public class SalariesController : HRBaseController
    {
        readonly IConfiguration _configuration;
        readonly ADOProcedures _ADOProcedures;
        readonly GenericRepository<Department> _DeptRepo;
        public SalariesController(IConfiguration configuration, GenericRepository<Department> repository)
        {
            _configuration = configuration;
            string? connectionString = _configuration.GetConnectionString("Default");
            _ADOProcedures = new ADOProcedures(connectionString);
            _DeptRepo = repository;
        }
        [HttpGet]
        public async Task<ActionResult<Pagination<SalaryObj>>> Get([FromQuery]SalariesParams P)
        {
            if (P.Year != 2024 || P.StartMonth > 03) return NotFound(new StatusResponse(404, $"Uneable to find salaries at {P.StartMonth}/{P.Year}"));
            if(P.StartMonth > P.EndMonth) return BadRequest(new StatusResponse(400, $"End date can't be before start date!"));
            var Data =  _ADOProcedures.GetSalaries(P);
            if(!Data.Any()) return BadRequest(new StatusResponse(404));
            return Ok(Data);
        }

        [HttpGet("department")]
        public async Task<ActionResult> GetDepartment(string name)
        {

            var specification = new DeptIncludeNavPropsSpecification(name);
            var department = await _DeptRepo.GetSpecified(specification);
            if (department is null) return NotFound(new StatusResponse(404, $"Uneable to find {name}"));
            var Departments = _ADOProcedures.GetDepartment(name);
            return Ok(Departments);
        }

    }
}
