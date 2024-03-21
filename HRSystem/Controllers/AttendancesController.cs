
namespace HRSystem.Controllers
{
    public class AttendancesController(GenericRepository<EmployeeAttendace> repository, IMapper mapper, GenericRepository<Employee> EmpRepo, GenericRepository<Department> DeptRepo) : HRBaseController
    {
        readonly GenericRepository<EmployeeAttendace> _AttendRepo = repository;
        readonly GenericRepository<Department> _DeptRepo = DeptRepo;
        readonly GenericRepository<Employee> _EmpRepo = EmpRepo;
        readonly IMapper mapper = mapper;

        [HttpGet("{Name}/{Date}", Name = "GetSpecificAttendanceRecord")]
        public async Task<ActionResult<GetDeptsDTO>> GetOneDept(string Name, string Date)
        {
            var specification = new AttendIncludeNavPropsSpecification(Name, DateOnlyOperations.ToDateOnly(Date));
            var Attend = await _AttendRepo.GetSpecified(specification);
            if (Attend == null) return NotFound(new StatusResponse(404));
            return Ok(mapper.Map<EmployeeAttendace, AttendDTO>(Attend));
        }
        [HttpGet]
        public async Task<ActionResult<Pagination<AttendDTO>>> GetAll([FromQuery] GetAllAttendancesParams P)
        {
            var specification = new AttendIncludeNavPropsSpecification(P);
            var Attends = await _AttendRepo.GetAllWithSpecificationsAsync(specification);
            if (P.Search != null)
            {
                Attends = Attends.Where(A => A.Employee.Name.ToLower().Contains(P.Search));
                Attends = Attends.Where(A => A.Employee.Department.Name.ToLower().Contains(P.Search));
            }
            var Data = mapper.Map<IEnumerable<EmployeeAttendace>, IEnumerable<AttendDTO>>(Attends);
            var countSpec = new CountAttendSpecification(P);
            var count = await _AttendRepo.GetCountAsync(countSpec);
            return Ok(new Pagination<AttendDTO>(P.PageIndex, P.PageSize, count, Data));
        }
        [HttpPost]
        public async Task<ActionResult<AttendDTO>> Create(AttendDTO attendDTO)
        {
            var specification = new EmpIncludeNavPropsSpecification(attendDTO.EmployeeName,0);
            var employee = await _EmpRepo.GetSpecified(specification);
            if (employee is null) return NotFound(new StatusResponse(404, $"{attendDTO.EmployeeName} can't be found"));
            if (!TimeSpanOperations.IsTime(attendDTO.ComingTime, attendDTO.LeaveTime))
                return BadRequest(new StatusResponse(400,"Invalid time format"));


            var attend = mapper.Map<EmployeeAttendace>(attendDTO);
            attend.Employee = employee;
            attend.Employee.Department = employee.Department;
            await _AttendRepo.AddAsync(attend);
            return Created();
        }

        [HttpPut("edit/{Name}/{Date}")]
        public async Task<ActionResult> Edit (string Name,string Date,AttendDTO attendDTO)
        {
            var specification = new AttendIncludeNavPropsSpecification(Name, DateOnlyOperations.ToDateOnly(Date));
            var Attendance = await _AttendRepo.GetSpecified(specification);
            if (Attendance is null) return NotFound(new StatusResponse(400));

            var attend = mapper.Map<EmployeeAttendace>(attendDTO);

            var department = Attendance.Employee.Department;

            TimeSpan timeToCome = TimeSpan.Parse(attendDTO.ComingTime);
            TimeSpan timeToLeave = TimeSpan.Parse(attendDTO.LeaveTime);

           var bonusHour= TimeSpanOperations.CalculateBonusHours(department.ComingTime, timeToCome, department.LeaveTime, timeToLeave);

          var discountHour=  TimeSpanOperations.CalculateDiscountHours(department.ComingTime, timeToCome, department.LeaveTime, timeToLeave);

            attend.Bonus = bonusHour;
            attend.Discount = discountHour;

            var employeeAttend = Attendance.Employee;
            if (employeeAttend is null) return NotFound(new StatusResponse(404, "Employee can't be found."));
            attend.Employee = employeeAttend;
            Expression<Func<EmployeeAttendace, bool>> predicate = a => a.Date == DateOnlyOperations.ToDateOnly(Date) && a.Employee.Name == Name;
            await _AttendRepo.UpdateAsync(predicate, Name, attend);

            return Ok(new StatusResponse(204, "Updated Successfully"));
        }
        [HttpDelete("delete/{Name}/{Date}")]
        public async Task<ActionResult> Delete (string Name, string Date) 
        {
            // Expression<Func<EmployeeAttendace, bool>> predicate = a => a.Date == DateOnlyOperations.ToDateOnly(Date) && a.Employee.Name == Name;
            var specification = new AttendIncludeNavPropsSpecification(Name, DateOnlyOperations.ToDateOnly(Date));
            var Attend = await _AttendRepo.GetSpecified(specification);
            if (Attend == null) return NotFound(new StatusResponse(404));
            await _AttendRepo.DeleteAsync(Attend);
            return Ok(new StatusResponse(204,"Deleted Successfully"));
        }

    } 
}
