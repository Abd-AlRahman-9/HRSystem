using AutoMapper;
using HRDomain.CustomConverter;
using HRDomain.Entities;
using HRDomain.Specification;
using HRRepository;
using HRSystem.DTO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.OpenApi.Writers;
using System.Globalization;
using System.Linq.Expressions;
using System.Text.RegularExpressions;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace HRSystem.Controllers
{
    public class HolidaysController : HRBaseController
    {
        private readonly GenericRepository<Vacation> _VacRepo;
        private readonly IMapper mapper;

        public HolidaysController(GenericRepository<Vacation> repository, IMapper mapper)
        {
            this._VacRepo = repository;
            this.mapper = mapper;
        }

        [HttpGet("{date}", Name = "GetSpecificHolidayByDate")]
        public async Task<ActionResult<OfficialHolidaysDTO>> GetHoliday(string date)
        {

            var specification = new VacIncludeNavPropsSpecification(DateOnlyCustomConverter.ToDateOnly(date));
            var Vac = await _VacRepo.GetSpecified(specification);
            return Ok(mapper.Map<Vacation, OfficialHolidaysDTO>(Vac));
        }

        [HttpPost]
        public async Task<ActionResult> Create(OfficialHolidaysDTO holidaysDTO)
        {
            if (!ModelState.IsValid) return BadRequest();

            var officialHoliday = mapper.Map<Vacation>(holidaysDTO);
           await _VacRepo.AddAsync(officialHoliday);

            string uri =  Url.Action(nameof(GetHoliday), new { officialHoliday.Date });
            return Created(uri,"Created Succsessfully");
        }

        [HttpPut("edit/{date}")]
        public async Task<ActionResult> Edit(string date, OfficialHolidaysDTO holidaysDTO)
        {
            var specification = new VacIncludeNavPropsSpecification(DateOnlyCustomConverter.ToDateOnly(date));
            var vacation = await _VacRepo.GetSpecified(specification);

            
            if (vacation is not null)
            {
                DateOnlyCustomConverter.ToDateOnly(holidaysDTO.DateOnTheCurrentYear);
                var Vac = mapper.Map<Vacation>(holidaysDTO);

                Expression<Func<Vacation, bool>> predicate = v => v.Date == DateOnlyCustomConverter.ToDateOnly(date);
               await _VacRepo.UpdateAsync(predicate,date,Vac);
                return StatusCode(202, "Updated Succsessfully");
            }
            return NotFound();
        }

        [HttpDelete("delete/{date}")]
        public async Task<ActionResult> Delete(string date)
        {
            string pattern = @"^\d{2}-\d{2}-\d{4}$";
            Regex regex = new Regex(pattern);
            if (regex.IsMatch(date))
            {

                Expression<Func<Vacation, bool>> predicate = v => v.Date==DateOnlyCustomConverter.ToDateOnly(date);
                await _VacRepo.DeleteAsync(predicate,date);
            return StatusCode(200, "Deleted Succsessfully");
            }
            return NotFound();
        }
    }
}
