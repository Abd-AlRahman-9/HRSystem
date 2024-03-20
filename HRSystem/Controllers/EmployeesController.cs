using HRDomain.Entities;
using HRSystem.DTO;

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
            var Data = mapper.Map<IEnumerable<Employee>, IEnumerable<EmployeesDTO>>(Emps);
            var countSpec = new CountEmpSpecification(P);
            var count = await _EmpRepo.GetCountAsync(countSpec);
            return Ok(new Pagination<EmployeesDTO>(P.PageIndex, P.PageSize, count, Data));
        }
        [HttpGet("{NationalId}", Name = "GetEmployeeByNatinalId")]
        public async Task<ActionResult<EmployeesDTO>> GetOneEmp(string NationalId)
        {
            var specification = new EmpIncludeNavPropsSpecification(NationalId);
            var Emp = await _EmpRepo.GetSpecified(specification);
            if (Emp is null) return NotFound(new StatusResponse(404, $"{NationalId} is not found"));
            return Ok(mapper.Map<Employee, EmployeesDTO>(Emp));
        }

        [HttpPost]
        public async Task<ActionResult> Create(EmployeesDTO employeesDTO)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            try
            {
                Expression<Func<Employee, bool>> predicate = e => e.NationalID == employeesDTO.NationalID;

                var hiring = DateOnlyOperations.ToDateOnly(employeesDTO.HiringDate);
                var birth = DateOnlyOperations.ToDateOnly(employeesDTO.HiringDate);

                if (!_EmpRepo.IsExist(predicate)) return BadRequest(new StatusResponse(400,"This National Id is already exist!"));
                var specification = new DeptIncludeNavPropsSpecification(employeesDTO.Department);
                var Dept = await _DeptRepo.GetSpecified(specification);
                int age = DateOnlyOperations.CheckAge(employeesDTO.DateOfBirth, employeesDTO.HiringDate);
                if (age < 20) return BadRequest(new StatusResponse(400,"Can't hire employee less than 20 years."));
                if (hiring.Year < 2008) return BadRequest(new StatusResponse(400,"Uneable to hire employee before establish the company!"));
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
                    BirthDate = birth,
                    HireData = hiring,
                    Department = Dept,
                };
                employee.manager = employee.Department.Manager;
               await _EmpRepo.AddAsync(employee);
              string url=  Url.Action(nameof(GetOneEmp),new {employee.NationalID});
                return Created(url, new StatusResponse(201,"New Employee has been created"));
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
            if (employee is null) return NotFound(new StatusResponse(404,$"{employeesDTO.NationalID} is not found"));

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
            employee.BirthDate = DateOnlyOperations.ToDateOnly(employeesDTO.DateOfBirth);
            employee.HireData = DateOnlyOperations.ToDateOnly(employeesDTO.HiringDate);
            employee.Department = await _DeptRepo.GetSpecified(specifications);


            Expression<Func<Employee, bool>> predicate = e => e.NationalID == ID;
           await _EmpRepo.UpdateAsync(predicate, ID, employee);
            return Ok(new StatusResponse(204,"Updated Successfully"));
        }

        [HttpDelete("delete/id")]
        public async Task<ActionResult> Delete(string ID)
        {
            var specification = new EmpIncludeNavPropsSpecification(ID);
            var employee = await _EmpRepo.GetSpecified(specification);
            if (employee is null) return NotFound(new StatusResponse(400, $"Uneable to find employee with {ID}, check national id and try again."));
           // Expression<Func<Employee, bool>> predicate = e => e.NationalID == ID;
           await _EmpRepo.DeleteAsync(employee);
            return Ok(new StatusResponse(204,"Deleted Successfully"));
        }
    }
}
