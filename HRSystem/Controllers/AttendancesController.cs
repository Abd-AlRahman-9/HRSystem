using AutoMapper;
using HRDomain.Entities;
using HRDomain.Specification;
using HRRepository;
using HRSystem.DTO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HRSystem.Controllers
{
    public class AttendancesController : HRBaseController
    {
        private readonly GenericRepository<EmployeeAttendace> _AttendRepo;
        private readonly Mapper mapper;

        public AttendancesController(GenericRepository<EmployeeAttendace> repository, Mapper mapper)
        {
            this._AttendRepo = repository;
            this.mapper = mapper;
        }
        [HttpGet("{Name:alpha}/{Date}", Name = "GetSpecificAttendanceRecord")]
        public async Task<ActionResult<GetDeptsDTO>> GetOneDept(string Name,DateOnly Date)
        {
            var specification = new AttendIncludeNavPropsSpecification(Name,Date);
            var Attend = await _AttendRepo.GetByNameAndDateWithSpecificationAsync(specification);
            return Ok(mapper.Map<EmployeeAttendace, AttendDTO>(Attend));
        }
    }
}
