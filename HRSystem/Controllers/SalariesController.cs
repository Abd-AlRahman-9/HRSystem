using AutoMapper;
using HRDomain.Entities.DrivenEntities;
using HRDomain.Specification.EntitiesSpecification;
using HRDomain.Specification.Params;

namespace HRSystem.Controllers
{

    public class SalariesController : HRBaseController
    {
        readonly IMapper mapper;
        readonly IConfiguration _configuration;
        readonly ADOProcedures _ADOProcedures;
        readonly GenericRepository<Department> _DeptRepo;
        readonly GenericRepository<Employee> _EmpRepo;
        public SalariesController(IConfiguration configuration, GenericRepository<Department> repository, GenericRepository<Employee> empRepo,IMapper _mapper)
        {
            _configuration = configuration;
            string? connectionString = _configuration.GetConnectionString("Default");
            _ADOProcedures = new ADOProcedures(connectionString);
            _DeptRepo = repository;
            _EmpRepo = empRepo;
            mapper = _mapper;
        }
        [HttpGet]
        public async Task<ActionResult<Pagination<SalaryObj>>> Get([FromQuery]SalariesParams P)
        {
            if (P.Year != 2024 || P.StartMonth >= 03) return NotFound(new StatusResponse(404, $"Uneable to find salaries at {P.StartMonth}/{P.Year}"));
            if(P.StartMonth > P.EndMonth) return BadRequest(new StatusResponse(400, $"End date can't be before start date!"));
            var Data =  _ADOProcedures.GetSalaries(P);
            if(!Data.Any()) return BadRequest(new StatusResponse(404));
<<<<<<< HEAD

=======
>>>>>>> 6099f6fe5bfb3f84baf1f62c97c56a70815de727
            return Ok(new Pagination<SalaryObj>(P.PageIndex, P.PageSize, _ADOProcedures.SalariesCount, Data));
        }
        [HttpGet("department")]
        public async Task<ActionResult> GetDepartment(string name)
        {

            var specification = new DeptIncludeNavPropsSpecification(name);
            var department = await _DeptRepo.GetSpecified(specification);
            if (department is null) return NotFound(new StatusResponse(404, $"Uneable to find {name}"));

            var Departments = _ADOProcedures.GetDepartment(name);

            Departments.Manager = _EmpRepo.GetByIdAsync(Departments.ManagerId.Value).Result;
            var dept = mapper.Map<GetDeptsDTO>(Departments);
            return Ok(dept);
        }
        [HttpGet("Absent")]
        public async Task<ActionResult> GetAbsent([FromQuery]SalProcedureParams P)
        {
            var Emp = EmployeeData(P.NationalId);
            if (Emp is null) return NotFound(new StatusResponse(404, "Uneable to find employee"));
            if (P.Year > DateTime.Now.Year) return NotFound(new StatusResponse(404, $"No absent days for {Emp.Result.Name} at {P.Year}"));
            var Absents = _ADOProcedures.GetAbsentDays(P);
            return Ok(Absents);
        }

        [HttpGet("Attend")]
        public async Task<ActionResult> GetAttend([FromQuery] SalProcedureParams P)
        {
            var Emp = EmployeeData(P.NationalId);
            if (Emp is null) return NotFound(new StatusResponse(404, $"Uneable to find employee"));
            if (P.Year > DateTime.Now.Year) return NotFound(new StatusResponse(404, $"No data about {Emp.Result.Name} at {P.Year}"));
            var Attend = _ADOProcedures.GetAttendToEmployee(P);
            return Ok(Attend);
        }

        [HttpGet("late")]
        public async Task<ActionResult> Late([FromQuery] SalProcedureParams P)
        {
            var Emp = EmployeeData(P.NationalId);
            if (Emp is null) return NotFound(new StatusResponse(404, $"Uneable to find employee"));
            if (P.Year > DateTime.Now.Year) return NotFound(new StatusResponse(404, $"No data about {Emp.Result.Name} at {P.Year}"));
            var late = _ADOProcedures.GetLateDays(P);
            return Ok(late);
        }

        private async Task<Employee> EmployeeData(string nationalId)
        {
            var specification = new EmpIncludeNavPropsSpecification(nationalId);
            var Emp =  _EmpRepo.GetSpecified(specification);
            return Emp.Result;
        }

    }
}
