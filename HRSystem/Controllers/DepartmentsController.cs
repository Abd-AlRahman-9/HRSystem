using AutoMapper;
using HRDomain.Entities;
using HRDomain.Specification;
using HRRepository;
using HRSystem.DTO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HRSystem.Controllers
{
    public class DepartmentsController : HRBaseController
    {
        private readonly GenericRepository<Department> _DeptRepo;
        private readonly Mapper mapper;

        public DepartmentsController(GenericRepository<Department> repository,Mapper mapper)
        {
            this._DeptRepo = repository;
            this.mapper = mapper;
        }
        [HttpGet]
        public async Task<ActionResult<IEnumerable<GetDeptsDTO>>> GetAllDepts([FromQuery]GetAllDeptsParams P) 
        {
            var specification = new DeptIncludeNavPropsSpecification(P);
            var Depts = await _DeptRepo.GetAllWithSpecificationsAsync(specification);
            return Ok(mapper.Map<IEnumerable<Department>,IEnumerable<GetDeptsDTO>>(Depts));
        }
    }
}
