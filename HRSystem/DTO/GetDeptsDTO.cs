using HRDomain.Entities;

namespace HRSystem.DTO
{
    public class GetDeptsDTO
    {
        public string Name { get; set; }
        public sbyte WorkDays { get; set; }
        public double DeductHour { get; set; }
        public double BonusHour { get; set; }
        public string ComingTime { get; set; }
        public string LeaveTime { get; set; }
        public string ManagerName { get; set; }
    }
}
