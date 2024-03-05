using AutoMapper;
using HRDomain.Entities;
using HRDomain.Specification;
using HRRepository;
using HRSystem.DTO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HRSystem.Controllers
{
    public class HolidaysController : HRBaseController
    {
        private readonly GenericRepository<Vacation> _VacRepo;
        private readonly Mapper mapper;

        public HolidaysController(GenericRepository<Vacation> repository, Mapper mapper)
        {
            this._VacRepo = repository;
            this.mapper = mapper;
        }

        [HttpGet("{Date}", Name = "GetSpecificHolidayByDate")]
        public async Task<ActionResult<OfficialHolidaysDTO>> GetHoliday(DateOnly date)
        {
            var specification = new VacIncludeNavPropsSpecification(date);
            var Vac = await _VacRepo.GetByDateWithSpecificationAsync(specification);
            return Ok(mapper.Map<Vacation, OfficialHolidaysDTO>(Vac));
        }
    }
}
