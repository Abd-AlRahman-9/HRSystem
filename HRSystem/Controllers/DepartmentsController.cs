using AutoMapper;
using HRDomain.Entities;
using HRDomain.Specification;
using HRRepository;
using HRSystem.DTO;
using HRSystem.Error_Handling;
using HRSystem.Helpers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Microsoft.SqlServer.Server;
using System;
using System.Globalization;
using System.Linq.Expressions;
using System.Text.RegularExpressions;

namespace HRSystem.Controllers
{
    public class DepartmentsController : HRBaseController
    {
        private readonly GenericRepository<Department> _DeptRepo;
        private readonly GenericRepository<Employee> _EmpRepo;
        private readonly IMapper mapper;

        public DepartmentsController(GenericRepository<Department> repository,GenericRepository<Employee> EmpRepo, IMapper mapper)
        {
            this._DeptRepo = repository;
            this._EmpRepo = EmpRepo;
            this.mapper = mapper;
        }
        [HttpGet("{Name:alpha}", Name = "GetDepartmentByName")]
        public async Task<ActionResult<GetDeptsDTO>> GetOneDept (string Name)
        {
            var specification = new DeptIncludeNavPropsSpecification(Name);
            var Dept = await _DeptRepo.GetSpecified(specification);
            if (Dept is null) return NotFound(new ErrorResponse(400));
            return Ok(mapper.Map<Department,GetDeptsDTO>(Dept));
        }
        [HttpGet]
        public async Task<ActionResult<Pagination<GetDeptsDTO>>> GetAllDepts([FromQuery]GetAllDeptsParams P) 
        {
            if (P.MngNationalId != null)
            {
                var Mng = await _EmpRepo.GetSpecified(new EmpIncludeNavPropsSpecification(P.MngNationalId));
                P.MngId = Mng.Id;
            }
            var specification = new DeptIncludeNavPropsSpecification(P);
            var Depts = await _DeptRepo.GetAllWithSpecificationsAsync(specification);
            var Data = mapper.Map<IEnumerable<Department>, IEnumerable<GetDeptsDTO>>(Depts);
            var countSpec = new CountDeptSpecification(P);
            var count = await _DeptRepo.GetCountAsync(countSpec);
            return Ok(new Pagination<GetDeptsDTO>(P.PageIndex,P.PageSize,count,Data));
        }

        [HttpPost ("WorkDays: int")]
        public async Task<ActionResult> Create(GetDeptsDTO deptsDTO, int workDays)
        {
            string pattern = @"^([0-1][0-9]|2[0-3]):([0-5][0-9]):([0-5][0-9])$";
            Regex regex = new Regex(pattern);

            if ((!regex.IsMatch(deptsDTO.ComingTime) && !regex.IsMatch(deptsDTO.TimeToLeave)) || !ModelState.IsValid)
                return BadRequest();

            deptsDTO.WorkDays = (sbyte)workDays;
            var department = mapper.Map<Department>(deptsDTO);
           // department.ManagerId = 

            await _DeptRepo.AddAsync(department);
            string uri = Url.Action(nameof(GetOneDept), new { id = department.Id });

            return Created(uri, "Created succsessfully");
        }

        [HttpPut("{name}")]
        public async Task<ActionResult> Edit(string name,GetDeptsDTO deptsDTO)
        {
            var specification = new DeptIncludeNavPropsSpecification(deptsDTO.DepartmentName);
            var department = await _DeptRepo.GetSpecified(specification);
            if (department is null)
            {
                return NotFound();
            }
           var dept= mapper.Map<Department>(deptsDTO);
            Expression<Func<Department, bool>> predicate = d => d.Name == name;
            await _DeptRepo.UpdateAsync(predicate,name,dept);
            return StatusCode(202);
        }

        [HttpDelete("delete/{name}")]
        public async Task<ActionResult> SoftDelete(string name)
        {
            var specification = new DeptIncludeNavPropsSpecification(name);
            var department = await _DeptRepo.GetSpecified(specification);
            if (department is null)
            {
                return NotFound();
            }
            Expression<Func<Department, bool>> predicate= d => d.Name == name;
               await _DeptRepo.DeleteAsync( predicate, name);

                return StatusCode(200, "Deleted Succsessfully");
        }
    }
}
