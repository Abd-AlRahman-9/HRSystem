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
    public class DepartmentsController : HRBaseController
    {
        private readonly GenericRepository<Department> _DeptRepo;
        private readonly Mapper mapper;

        public DepartmentsController(GenericRepository<Department> repository, Mapper mapper)
        {
            this._DeptRepo = repository;
            this.mapper = mapper;
        }
        [HttpGet("{Name:alpha}", Name = "GetDepartmentByName")]
        public async Task<ActionResult<GetDeptsDTO>> GetOneDept (string Name)
        {
            var specification = new DeptIncludeNavPropsSpecification(Name);
            var Dept = await _DeptRepo.GetByNameWithSpecificationAsync(specification);
            return Ok(mapper.Map<Department,GetDeptsDTO>(Dept));
        }
        [HttpGet]
        public async Task<ActionResult<Pagination<GetDeptsDTO>>> GetAllDepts([FromQuery]GetAllDeptsParams P) 
        {
            var specification = new DeptIncludeNavPropsSpecification(P);
            var Depts = await _DeptRepo.GetAllWithSpecificationsAsync(specification);
            var Data = mapper.Map<IEnumerable<Department>, IEnumerable<GetDeptsDTO>>(Depts);
            var countSpec = new CountDeptSpecification(P);
            var count = await _DeptRepo.GetCountAsync(countSpec);
            return Ok(new Pagination<GetDeptsDTO>(P.PageIndex,P.PageSize,count,Data));
        }
        //[HttpGet("{id:int}", Name = ("GetDepartmentById"))]
        //public async Task<ActionResult> GetById(int id)
        //{
        //    var department = await _DeptRepo.GetByIdAsync(id);
        //    if(department is null)
        //    {
        //        return BadRequest("Not Found!");
        //    }
        //    GetDeptsDTO deptsDTO = new();
        //    deptsDTO = mapper.Map<GetDeptsDTO>(department);
        //    return Ok(deptsDTO);
        //}
        [HttpPost]
        public async Task<ActionResult> Create(GetDeptsDTO deptsDTO)
        {
            if (ModelState.IsValid)
            {
                var department = mapper.Map<Department>(deptsDTO);
              await _DeptRepo.AddAsync(department);
              string uri=  Url.Action(nameof(GetOneDept), new { id = department.Id });
               
                return Created(uri,"Created succsessfully");
            }
            return BadRequest();
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> Edit(int id,GetDeptsDTO deptsDTO)
        {
            var department = _DeptRepo.GetByIdAsync(id);
            if (department is null)
            {
                return NotFound();
            }
           var dept= mapper.Map<Department>(deptsDTO);
           await _DeptRepo.UpdateAsync(id, dept);
            return StatusCode(202);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> SoftDelete(int id)
        {
            var department = _DeptRepo.GetByIdAsync(id);
            if (department is not null)
            {
              await _DeptRepo.DeleteAsync(id);
                return StatusCode(200, "Deleted Succsessfully");
            }
            return NotFound("Element isn't exist");
        }
    }
}
