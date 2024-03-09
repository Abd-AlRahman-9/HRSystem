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
        private readonly GenericRepository<Department> _DeptRepo;
        private readonly GenericRepository<Employee> _EmpRepo;
        private readonly IMapper mapper;

        public EmployeesController(GenericRepository<Employee> repository,GenericRepository<Department> DeptRepo,IMapper mapper)
        {
            this._DeptRepo = DeptRepo;
            this._EmpRepo = repository;
            this.mapper = mapper;
        }
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
                Employee employee = mapper.Map<Employee>(employeesDTO);
               await _EmpRepo.AddAsync(employee);
                return Created();
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message,ex);
            }

            
            
        }
    }
}
