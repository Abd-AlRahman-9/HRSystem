namespace HRSystem.Controllers
{
    public class EmployeesController(GenericRepository<Employee> repository, GenericRepository<Department> DeptRepo, IMapper mapper) : HRBaseController
    {
        private readonly GenericRepository<Department> _DeptRepo = DeptRepo;
        private readonly GenericRepository<Employee> _EmpRepo = repository;
        private readonly IMapper mapper = mapper;

        [HttpGet]
        public async Task<ActionResult<Pagination<EmployeesDTO>>> GetAllEmps([FromQuery] GetAllEmpsParams P)
        {
            if (P.DeptName != null)
            {
                var Dept = await _DeptRepo.GetSpecified(new DeptIncludeNavPropsSpecification(P.DeptName));
                P.DeptId = Dept.Id;
            }
            if (P.NationalID != null)
            {
                var Emp = await _EmpRepo.GetSpecified(new EmpIncludeNavPropsSpecification(P.NationalID));
                P.MngId = Emp.Id;
            }
            var specification = new EmpIncludeNavPropsSpecification(P);
            var Emps = await _EmpRepo.GetAllWithSpecificationsAsync(specification); 
            var Data = mapper.Map<IEnumerable<Employee>, IEnumerable<GetDeptsDTO>>(Emps);
            var countSpec = new CountEmpSpecification(P);
            var count = await _EmpRepo.GetCountAsync(countSpec);
            return Ok(new Pagination<GetDeptsDTO>(P.PageIndex, P.PageSize, count, Data));
        }
        [HttpGet("{NationalId}", Name = "GetEmployeeByNatinalId")]
        public async Task<ActionResult<EmployeesDTO>> GetOneEmp(string NationalId)
        {
            var specification = new EmpIncludeNavPropsSpecification(NationalId);
            var Emp = await _EmpRepo.GetSpecified(specification);
            return Ok(mapper.Map<Employee, EmployeesDTO>(Emp));
        }

        [HttpPost]
        public async Task<ActionResult> Create(EmployeesDTO employeesDTO)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            try
            {
                var specification = new DeptIncludeNavPropsSpecification(employeesDTO.Department);
                var Dept = await _DeptRepo.GetSpecified(specification);
                //Employee employee = mapper.Map<Employee>(employeesDTO);
                Employee employee = new()
                {
                    Name = employeesDTO.EmployeeName,
                    Address = employeesDTO.Address,
                    Gender = employeesDTO.Gender,
                    NationalID = employeesDTO.NationalID,
                    Nationality = employeesDTO.Nationality,
                    PhoneNumber = employeesDTO.Phone,
                    VacationsRecord = employeesDTO.VacationsCredit,
                    Salary = employeesDTO.Salary,
                    BirthDate = DateOnlyOperations.ToDateOnly(employeesDTO.DateOfBirth),
                    HireData = DateOnlyOperations.ToDateOnly(employeesDTO.HiringDate),
                    Department = Dept,
                };
                employee.manager = employee.Department.Manager;
               await _EmpRepo.AddAsync(employee);
              string url=  Url.Action(nameof(GetOneEmp),new {employee.NationalID});
                return Created(url, "Created Succsessfully");
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message,ex);
            } 
        }

        [HttpPut("Edit/id")]
        public async Task<ActionResult> Edit(string ID, EmployeesDTO employeesDTO)
        {
            var specification = new EmpIncludeNavPropsSpecification(ID);
            var employee = await _EmpRepo.GetSpecified(specification);
            if (employee is null) return NotFound(new ErrorResponse(404,$"{employeesDTO.NationalID} is not found"));

            //Employee emp= mapper.Map<Employee>(employeeDTO);
            var specifications = new DeptIncludeNavPropsSpecification(employeesDTO.Department);

            employee.Name = employeesDTO.EmployeeName;
            employee.Address = employeesDTO.Address;
            employee.Gender = employeesDTO.Gender;
            employee.NationalID = employeesDTO.NationalID;
            employee.Nationality = employeesDTO.Nationality;
            employee.PhoneNumber = employeesDTO.Phone;
            employee.Salary = employeesDTO.Salary;
            employee.VacationsRecord = employeesDTO.VacationsCredit;
            employee.Department = await _DeptRepo.GetSpecified(specifications);


            Expression<Func<Employee, bool>> predicate = e => e.NationalID == ID;
           await _EmpRepo.UpdateAsync(predicate, ID, employee);
            return StatusCode(202);
        }

        [HttpDelete("delete/id")]
        public async Task<ActionResult> Delete(string ID)
        {
            Expression<Func<Employee, bool>> predicate = e => e.NationalID == ID;
           await _EmpRepo.DeleteAsync(predicate,ID);
            return StatusCode(202, "Deleted Succsessfully");
        }
    }
}
