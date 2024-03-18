namespace HRSystem.DTO
{
    public class GetDeptsDTO
    {
        public string DepartmentName { get; set; }
        public sbyte WorkDays { get; set; }
        public double DeductionRule { get; set; }
        public double BonusRule { get; set; } 
        public string ComingTime { get; set; }
        public string TimeToLeave { get; set; }
        //public string FirstOfficalHoliday { get; set; }
        //public string SecondOfficalHoliday { get; set; }

        public string ManagerName { get; set; }
        public bool IsHourly { get; set; } 
    }
}
