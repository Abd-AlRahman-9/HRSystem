using System.ComponentModel.DataAnnotations;

namespace HRSystem.DTO
{
    public class OfficialHolidaysDTO
    {
        public string HolidayName { get; set; }
        public string DateOnTheCurrentYear { get; set; }
    }
}
