using HRDomain.Entities;
using HRDomain.Specification.EntitiesSpecification;
using HRDomain.Specification.Params;

namespace HRSystem.Controllers
{
    public class DepartmentsController: HRBaseController
    { 
        readonly GenericRepository<Department> _DeptRepo;
        readonly GenericRepository<Employee> _EmpRepo ;
        readonly IMapper mapper;
        readonly IConfiguration _configuration;
        readonly ADOProcedures aDOProcedures;
        public DepartmentsController(GenericRepository<Department> repository, GenericRepository<Employee> EmpRepo, IMapper mapper,IConfiguration configuration)
        {
            _DeptRepo = repository;
            _EmpRepo = EmpRepo;
            this.mapper = mapper;
            _configuration = configuration;
            string? connectionString =  _configuration.GetConnectionString("Default");
            aDOProcedures = new ADOProcedures(connectionString);
        }

        [HttpGet("{Name}", Name = "GetDepartmentByName")]
        public async Task<ActionResult<GetDeptsDTO>> GetOneDept (string Name)
        {
            var Dept = await FindDepartment(Name);
            if (Dept is null) return NotFound(new StatusResponse(404));
            return Ok(mapper.Map<Department,GetDeptsDTO>(Dept));
        }
        [HttpGet]
        public async Task<ActionResult<GetDeptsDTO>> GetAllDepts([FromQuery]GetAllDeptsParams P) 
        {
            if (P.MngNationalId != null)
            {
                var Mng = await _EmpRepo.GetSpecified(new EmpIncludeNavPropsSpecification(P.MngNationalId));
                P.MngId = Mng.Id;
            }
            var specification = new DeptIncludeNavPropsSpecification(P);
            var Depts = await _DeptRepo.GetAllWithSpecificationsAsync(specification);
            var Data = mapper.Map<IEnumerable<Department>, IEnumerable<GetDeptsDTO>>(Depts);
            return Ok(Data);
        }

        [HttpPost ("WorkDays")]
        public async Task<ActionResult> Create(GetDeptsDTO deptsDTO, int workDays)
        {
            Expression<Func<Department, bool>> predicate = d => d.Name == deptsDTO.DepartmentName;
            if (!_DeptRepo.IsExist(predicate)) return BadRequest(new StatusResponse(400, $"{deptsDTO.DepartmentName} is already exist!"));

            if (!TimeSpanOperations.IsTime(deptsDTO.ComingTime, deptsDTO.TimeToLeave))
                return BadRequest("Invalid time format,Please provide the time in the format 'hh:mm:ss'");

            deptsDTO.WorkDays = (sbyte)workDays;
            
            var department = mapper.Map<Department>(deptsDTO);
            var manger = SetManger(deptsDTO.ManagerName);
            if(manger is null) return NotFound(new StatusResponse(404, $"Please check that {deptsDTO.ManagerName} is exist and isn't a manger of other department"));
            department.Manager = manger;
            
            await _DeptRepo.AddAsync(department);
            string? uri =  Url.Action(nameof(GetOneDept), new { id = department.Id });

            return Created(uri, new StatusResponse(201,"New Department has been created"));
        }

        [HttpPut("Edit/{name}")]
        public async Task<ActionResult> Edit(string name,GetDeptsDTO deptsDTO)
        {
            var department = await FindDepartment(name);
            if (department is null) return NotFound(new StatusResponse(404, $"Uneable to find {name} department"));


            if (!TimeSpanOperations.IsTime(deptsDTO.ComingTime, deptsDTO.TimeToLeave))
                return BadRequest(new StatusResponse(400,"Invalid time format,Please provide the time in the format 'hh:mm:ss'"));


            var dept = mapper.Map<Department>(deptsDTO);
            string lowerName ;
            if (!department.Manager.Name.Equals(deptsDTO.ManagerName))
            {
                var manger = SetManger(deptsDTO.ManagerName);
            if (manger == null) return NotFound(new StatusResponse(404, $"Please check that {deptsDTO.ManagerName} is exist and isn't a manger of other department."));
                lowerName = manger.Name.ToLower();
            if ( dept.Manager.Name.ToLower() ==lowerName) return NotFound(new StatusResponse(404, $"{deptsDTO.ManagerName} is the manger of {manger.Department.Manager.Name}."));

            }
            else
            {
                var employee = new EmpIncludeNavPropsSpecification(name, 0);
               dept.Manager = await _EmpRepo.GetSpecified(employee);
            }
            

            Expression<Func<Department, bool>> predicate = d => d.Name == name;
            await _DeptRepo.UpdateAsync(predicate, name, dept);
            return Ok(new StatusResponse(204,"Updated Successfully"));
        }

        [HttpDelete("delete/{name}")]
        public async Task<ActionResult> SoftDelete(string name)
        {
            var department = await FindDepartment(name);
            if (department is null) return NotFound(new StatusResponse(404, $"Uneable to find {name} department"));

               await _DeptRepo.DeleteAsync( department);
               return Ok( new StatusResponse(204,"Deleted Successfully"));
        }
        private async Task<Department> FindDepartment(string name)
        {
            var specification = new DeptIncludeNavPropsSpecification(name);
            var department = await _DeptRepo.GetSpecified(specification);
            return department;
        }
        private Employee SetManger (string name)
        {
            var manger = new EmpIncludeNavPropsSpecification(name,0);
            var departmentManger =  _EmpRepo.GetSpecified(manger);
            if ( departmentManger != null)
            {
              var emp = aDOProcedures.GetManagers().FirstOrDefault(m => m.Value == name.ToLower());
                if (emp.Key != null)
                    return null;
                else return departmentManger.Result;
            }
            return  departmentManger.Result;
        }
    }
}
