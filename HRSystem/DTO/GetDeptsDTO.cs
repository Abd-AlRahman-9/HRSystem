using HRDomain.Entities;

namespace HRSystem.DTO
{
    public class GetDeptsDTO
    {
        public string DepartmentName { get; set; }
        public sbyte WorkDays { get; set; }
        public decimal DeductHour { get; set; }
        public decimal BonusHour { get; set; }
        public double DeductionRule { get; set; }
        public double BonusRule { get; set; } 
        public TimeSpan ComingTime { get; set; }
        public TimeSpan TimeToLeave { get; set; }
        public string ManagerName { get; set; }
        public bool IsHourly { get; set; } 
    }
}
