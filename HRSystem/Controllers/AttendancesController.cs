using AutoMapper;
using HRDomain.Entities;
using HRDomain.Specification;
using HRRepository;
using HRSystem.DTO;
using HRSystem.Helpers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HRSystem.Controllers
{
    public class AttendancesController : HRBaseController
    {
        private readonly GenericRepository<EmployeeAttendace> _AttendRepo;
        private readonly GenericRepository<Department> _DeptRepo;
        private readonly GenericRepository<Employee> _EmpRepo;
        private readonly Mapper mapper;

        public AttendancesController(GenericRepository<EmployeeAttendace> repository, Mapper mapper,GenericRepository<Employee> EmpRepo,GenericRepository<Department> DeptRepo)
        {
            this._DeptRepo = DeptRepo;
            this._EmpRepo = EmpRepo;
            this._AttendRepo = repository;
            this.mapper = mapper;
        }
        [HttpGet("{Name:alpha}/{Date}", Name = "GetSpecificAttendanceRecord")]
        public async Task<ActionResult<GetDeptsDTO>> GetOneDept(string Name,DateOnly Date)
        {
            var specification = new AttendIncludeNavPropsSpecification(Name,Date);
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

    }
}
