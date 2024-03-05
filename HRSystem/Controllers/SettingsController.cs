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
    public class SettingsController : HRBaseController
    {
        private readonly GenericRepository<Department> _DeptRepo;
        private readonly Mapper mapper;

        public SettingsController(GenericRepository<Department> repository, Mapper mapper)
        {
            this._DeptRepo = repository;
            this.mapper = mapper;
        }
        [HttpGet]
        public async Task<ActionResult<Pagination<SettingsDTO>>> GetAllSettings([FromQuery] GetAllDeptsParams P)
        {
            var specification = new SetIncludeNavPropsSpecification(P);
            var Sets = await _DeptRepo.GetAllWithSpecificationsAsync(specification);
            var Data = mapper.Map<IEnumerable<Department>, IEnumerable<SettingsDTO>>(Sets);
            var countSpec = new CountSetSpecification(P);
            var count = await _DeptRepo.GetCountAsync(countSpec);
            return Ok(new Pagination<SettingsDTO>(P.PageIndex, P.PageSize, count, Data));
        }
        [HttpGet("{id:int}", Name = ("GetSpecificSettings"))]
        public async Task<ActionResult> GetById(int id)
        {
            var specification = new SetIncludeNavPropsSpecification(id);
            var Set = await _DeptRepo.GetByIdWithSpecificationAsync(specification);
            return Ok(mapper.Map<Department, SettingsDTO>(Set));
        }
    }
}
