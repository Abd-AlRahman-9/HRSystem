using System.Text.RegularExpressions;

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
            if (Vac == null) return NotFound(new ErrorResponse(400, $"{date} can't be found!"));
            return Ok(mapper.Map<Vacation, OfficialHolidaysDTO>(Vac));
        }
        //-----------------------------------------------------------//
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
                return Created(uri, "Created Succsessfully");
            }
           return BadRequest(new ErrorResponse(400, $"Date must be from " +
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
                    return BadRequest(new ErrorResponse(400, $"Date must be from " +
                        $"{DateTime.Now.Year} to 12/2999"));
                Expression<Func<Vacation, bool>> predicate = v => v.Date == DateOnlyOperations.ToDateOnly(date);

               await _VacRepo.UpdateAsync(predicate,date,Vac);
                return StatusCode(202, "Updated Succsessfully");
            }
            return NotFound(new ErrorResponse(404));
        }

        [HttpDelete("delete/{date}")]
        public async Task<ActionResult> Delete(string date)
        {
            string pattern = @"^\d{2}-\d{2}-\d{4}$";
            Regex regex = new Regex(pattern);

            if (regex.IsMatch(date))
            {

                Expression<Func<Vacation, bool>> predicate = v => v.Date == DateOnlyOperations.ToDateOnly(date);
                await _VacRepo.DeleteAsync(predicate, date);
                return StatusCode(200, "Deleted Succsessfully");
            }
            return NotFound(new ErrorResponse(404));
        }
    }
}
