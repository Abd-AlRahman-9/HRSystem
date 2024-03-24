using System.Text.RegularExpressions;
using HRDomain.Specification.EntitiesSpecification;

namespace HRSystem.Controllers
{
    public class HolidaysController(GenericRepository<Vacation> repository, IMapper mapper) : HRBaseController
    {
        private readonly GenericRepository<Vacation> _VacRepo = repository;
        private readonly IMapper mapper = mapper;

        [HttpGet("{date}", Name = "GetSpecificHolidayByDate")]
        public async Task<ActionResult<OfficialHolidaysDTO>> GetHoliday(string date)
        {

            var specification = new VacIncludeNavPropsSpecification(DateOnlyOperations.ToDateOnly(date));
            var Vac = await _VacRepo.GetSpecified(specification);
            if (Vac == null) return NotFound(new StatusResponse(400, $"{date} can't be found!"));
            return Ok(mapper.Map<Vacation, OfficialHolidaysDTO>(Vac));
        }
        [HttpGet]
        public async Task<ActionResult<OfficialHolidaysDTO>> GetAllDepts()
        {
            
            var Holidays = await _VacRepo.GetAllWithSpecificationsAsync(new VacIncludeNavPropsSpecification());
            var Data = mapper.Map<IEnumerable<Vacation>, IEnumerable<OfficialHolidaysDTO>>(Holidays);
            return Ok(Data);
        }


       [HttpPost]
        public async Task<ActionResult> Create(OfficialHolidaysDTO holidaysDTO)
        {
            if (!ModelState.IsValid) return BadRequest(400);

            DateOnlyOperations.ToDateOnly(holidaysDTO.DateOnTheCurrentYear);
            var officialHoliday = mapper.Map<Vacation>(holidaysDTO);
            //check if date is future date or past date
            var date = officialHoliday.Date;
            if (DateOnlyOperations.IsValidDate(date.Year))
            {
                await _VacRepo.AddAsync(officialHoliday);

                string uri = Url.Action(nameof(GetHoliday), new { officialHoliday.Date });
                return Created(uri, new StatusResponse(201));
            }
           return BadRequest(new StatusResponse(400, $"Date must be from " +
               $"{DateTime.Now.Year} to 12/2999"));
        }

        [HttpPut("edit/{date}")]
        public async Task<ActionResult> Edit(string date, OfficialHolidaysDTO holidaysDTO)
        {
            var specification = new VacIncludeNavPropsSpecification(DateOnlyOperations.ToDateOnly(date));
            var vacation = await _VacRepo.GetSpecified(specification);

            
            if (vacation is not null)
            {
              var currentDate=  DateOnlyOperations.ToDateOnly(holidaysDTO.DateOnTheCurrentYear);
                var Vac = mapper.Map<Vacation>(holidaysDTO);
                if (!DateOnlyOperations.IsValidDate(currentDate.Year))
                    return BadRequest(new StatusResponse(400, $"Date must be from " +
                        $"{DateTime.Now.Year} to 12/2999"));
                Expression<Func<Vacation, bool>> predicate = v => v.Date == DateOnlyOperations.ToDateOnly(date);

               await _VacRepo.UpdateAsync(predicate,date,Vac);
                return Ok(new StatusResponse(204, "Updated Successfully"));
            }
            return NotFound(new StatusResponse(404));
        }

        [HttpDelete("delete/{date}")]
        public async Task<ActionResult> Delete(string date)
        {
            var specification = new VacIncludeNavPropsSpecification(DateOnlyOperations.ToDateOnly(date));
            var Vac = await _VacRepo.GetSpecified(specification);
            if (Vac == null) return NotFound(new StatusResponse(400, $"{date} can't be found!"));
            //string pattern = @"^\d{2}-\d{2}-\d{4}$";
            //Regex regex = new Regex(pattern);

                //Expression<Func<Vacation, bool>> predicate = v => v.Date == DateOnlyOperations.ToDateOnly(date);
                await _VacRepo.DeleteAsync(Vac);
                return Ok(new StatusResponse(204, "Deleted Successfully"));
        }
    }
}
