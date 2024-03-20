namespace HRSystem.Controllers
{
    public class DepartmentsController(GenericRepository<Department> repository, GenericRepository<Employee> EmpRepo, IMapper mapper) : HRBaseController
    {
        readonly GenericRepository<Department> _DeptRepo = repository;
        readonly GenericRepository<Employee> _EmpRepo = EmpRepo;
        readonly IMapper mapper = mapper;

        //        readonly IConfiguration _configuration;
        //        readonly ADOProcedures aDOProcedures;
        //_configuration = configuration;
        //            string connectionString = _configuration.GetConnectionString("Default");
        //            aDOProcedures = new ADOProcedures(connectionString);

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
            //if (!TimeSpanOperations.IsTime(deptsDTO.ComingTime, deptsDTO.TimeToLeave))
            //    return BadRequest("Invalid time format,Please provide the time in the format 'hh:mm:ss'");
            if(TimeSpanOperations.Compare(deptsDTO.ComingTime, deptsDTO.TimeToLeave) < 6 )
                return BadRequest(new StatusResponse(400,"Working Hours can't be less than 6 hours"));
            //remember to create bool method for previous 2 validations, then concat them at one if state
            deptsDTO.WorkDays = (sbyte)workDays;
            var department = mapper.Map<Department>(deptsDTO);
            department.Manager = null;
            
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
                return BadRequest("Invalid time format,Please provide the time in the format '00:00:00'");
            if (TimeSpanOperations.Compare(deptsDTO.ComingTime, deptsDTO.TimeToLeave) < 6)
                return BadRequest(new StatusResponse(400, "Working Hours can't be less than 6 hours"));

            var dept = mapper.Map<Department>(deptsDTO);
            var manger = new EmpIncludeNavPropsSpecification(deptsDTO.ManagerName, department.Id);
            Employee departmentManger = await _EmpRepo.GetSpecified(manger);
            if (departmentManger is null) return NotFound(new StatusResponse(404, "Manger Name can't be found."));
            dept.Manager = departmentManger;

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
    }
}
