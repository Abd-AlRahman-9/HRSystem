using HRDomain.Entities;

namespace HRSystem.DTO
{
    public class GetDeptsDTO
    {
        public string DepartmentName { get; set; }
        public sbyte WorkDays { get; set; }
<<<<<<< HEAD
        public double DeductHour { get; set; }
        public double BonusHour { get; set; }
        public string ComingTime { get; set; }
        public string LeaveTime { get; set; }
=======
        public double DeductionRule { get; set; }
        public double BonusRule { get; set; }
        public TimeSpan ComingTime { get; set; }
        public TimeSpan TimeToLeave { get; set; }
>>>>>>> a694c65bce676ceef0f7634a0a9a300aa337b93e
        public string ManagerName { get; set; }
    }
}
