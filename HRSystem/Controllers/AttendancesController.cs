
using HRDomain.Entities;
using HRDomain.Specification.EntitiesSpecification;
using HRDomain.Specification.PaginatioCount;
using HRDomain.Specification.Params;

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
            //search about employee
            var specification = new EmpIncludeNavPropsSpecification(attendDTO.EmployeeName,0);
            var employee = await _EmpRepo.GetSpecified(specification);
            if (employee is null) return NotFound(new StatusResponse(404, $"{attendDTO.EmployeeName} can't be found"));
            if (!TimeSpanOperations.IsTime(attendDTO.ComingTime, attendDTO.LeaveTime))
                return BadRequest(new StatusResponse(400,"Invalid time format"));

            //separate
            var attend = mapper.Map<EmployeeAttendace>(attendDTO);
            attend.Employee = employee;
            var bonusHour = TimeSpanOperations.CalculateBonusHours(employee.Department.ComingTime, attendDTO.ComingTime, employee.Department.LeaveTime, attendDTO.LeaveTime);
            var discountHour = TimeSpanOperations.CalculateDiscountHours(employee.Department.ComingTime, attendDTO.ComingTime, employee.Department.LeaveTime, attendDTO.LeaveTime);

            attend.Bonus = bonusHour;
            attend.Discount = discountHour;
            attend.Date = DateOnly.FromDateTime(DateTime.Now.Date);
            await _AttendRepo.AddAsync(attend);
            return Ok(new StatusResponse(201,"Created Successfully"));
        }

        [HttpPut("edit/{Name}/{Date}")]
        public async Task<ActionResult> Edit (string Name,string Date,AttendDTO attendDTO)
        {
            var date = DateOnlyOperations.ToDateOnly(attendDTO.DateOfTheDay);
            var specification = new AttendIncludeNavPropsSpecification(Name, DateOnlyOperations.ToDateOnly(Date));
            var Attendance = await _AttendRepo.GetSpecified(specification);
            if (Attendance is null) return NotFound(new StatusResponse(400));
            if (date > DateOnly.FromDateTime(DateTime.Now.Date))
                return BadRequest(new StatusResponse(400, "Uneable to enter date hasn't come."));
            var attend = mapper.Map<EmployeeAttendace>(attendDTO);

            var department = Attendance.Employee.Department;


            var bonusHour= TimeSpanOperations.CalculateBonusHours(department.ComingTime, attendDTO.ComingTime, department.LeaveTime, attendDTO.LeaveTime);

          var discountHour=  TimeSpanOperations.CalculateDiscountHours(department.ComingTime, attendDTO.ComingTime, department.LeaveTime, attendDTO.LeaveTime);

            attend.Bonus = bonusHour;
            attend.Discount = discountHour;

            var employee = Attendance.Employee;
            if (employee is null) return NotFound(new StatusResponse(404, "Employee can't be found."));
            attend.Employee = employee;
            Expression<Func<EmployeeAttendace, bool>> predicate = a => a.Date == DateOnlyOperations.ToDateOnly(Date) && a.Employee.Name == Name;
            await _AttendRepo.UpdateAsync(predicate, Name, attend);

            return Ok(new StatusResponse(204, "Updated Successfully"));
        }
        [HttpDelete("delete/{Name}/{Date}")]
        public async Task<ActionResult> Delete (string Name, string Date) 
        {
            var specification = new AttendIncludeNavPropsSpecification(Name, DateOnlyOperations.ToDateOnly(Date));
            var Attend = await _AttendRepo.GetSpecified(specification);
            if (Attend == null) return NotFound(new StatusResponse(404));
            await _AttendRepo.DeleteAsync(Attend);
            return Ok(new StatusResponse(204,"Deleted Successfully"));
        }

    } 
}
