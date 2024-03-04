using HRDomain.Entities;

namespace HRSystem.DTO
{
    public class GetDeptsDTO
    {
        public string Name { get; set; }
        public sbyte WorkDays { get; set; }
        public double DeductHour { get; set; }
        public double BonusHour { get; set; }
        public TimeSpan ComingTime { get; set; }
        public TimeSpan LeaveTime { get; set; }
        public string ManagerName { get; set; }
    }
}
