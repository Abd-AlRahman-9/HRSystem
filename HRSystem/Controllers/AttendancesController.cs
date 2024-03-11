using AutoMapper;
using HRDomain.CustomConverter;
using HRDomain.Entities;
using HRDomain.Specification;
using HRRepository;
using HRSystem.DTO;
using HRSystem.Error_Handling;
using HRSystem.Helpers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Linq.Expressions;

namespace HRSystem.Controllers
{
    public class AttendancesController : HRBaseController
    {
        private readonly GenericRepository<EmployeeAttendace> _AttendRepo;
        private readonly GenericRepository<Department> _DeptRepo;
        private readonly GenericRepository<Employee> _EmpRepo;
        private readonly IMapper mapper;

        public AttendancesController(GenericRepository<EmployeeAttendace> repository, IMapper mapper, GenericRepository<Employee> EmpRepo, GenericRepository<Department> DeptRepo)
        {
            this._DeptRepo = DeptRepo;
            this._EmpRepo = EmpRepo;
            this._AttendRepo = repository;
            this.mapper = mapper;
        }
        [HttpGet("{Name:alpha}/{Date}", Name = "GetSpecificAttendanceRecord")]
        public async Task<ActionResult<GetDeptsDTO>> GetOneDept(string Name, string Date)
        {
            var specification = new AttendIncludeNavPropsSpecification(Name, DateOnlyOperations.ToDateOnly(Date));
            var Attend = await _AttendRepo.GetSpecified(specification);
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
        public async Task<ActionResult> Create(AttendDTO attendDTO)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

           var Attenadance= mapper.Map<EmployeeAttendace>(attendDTO);

            await _AttendRepo.AddAsync(Attenadance);
          string url=  Url.Action(nameof(GetOneDept), new {Attenadance.Employee.Name, Attenadance.Date });
            return Created(url,"Created Succsessfully");

        }
        [HttpPut("edit/{Name}/{Date}")]
        public async Task<ActionResult> Edit (string Name,string Date,AttendDTO attendDTO)
        {
            var specification = new AttendIncludeNavPropsSpecification(Name, DateOnlyOperations.ToDateOnly(Date));
            var Attendance = await _AttendRepo.GetSpecified(specification);
            if (Attendance is null) return NotFound(new ErrorResponse(400));

           var attend= mapper.Map<EmployeeAttendace>(attendDTO);
            Expression<Func<EmployeeAttendace, bool>> predicate = a => a.Date == DateOnlyOperations.ToDateOnly(Date) && a.Employee.Name == Name;
           await _AttendRepo.UpdateAsync(predicate,Name,attend);
            return StatusCode(202, "Updated Succsessfully");
        }
        [HttpDelete("delete/{Name}/{Date}")]
        public async Task<ActionResult> Delete (string Name, string Date) 
        {
            Expression<Func<EmployeeAttendace, bool>> predicate = a => a.Date == DateOnlyOperations.ToDateOnly(Date) && a.Employee.Name == Name;
           await _AttendRepo.DeleteAsync(predicate,Name);
            return StatusCode(202);
        }

    } 
}
