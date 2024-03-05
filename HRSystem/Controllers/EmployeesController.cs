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
    public class EmployeesController : HRBaseController
    {
        private readonly GenericRepository<Employee> _EmpRepo;
        private readonly Mapper mapper;

        public EmployeesController(GenericRepository<Employee> repository, Mapper mapper)
        {
            this._EmpRepo = repository;
            this.mapper = mapper;
        }
        [HttpGet]
        public async Task<ActionResult<Pagination<EmployeesDTO>>> GetAllEmps([FromQuery] GetAllEmpsParams P)
        {
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
            var Emp = await _EmpRepo.GetByNameWithSpecificationAsync(specification);
            return Ok(mapper.Map<Employee, EmployeesDTO>(Emp));
        }
    }
}
