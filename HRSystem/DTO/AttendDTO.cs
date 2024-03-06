namespace HRSystem.DTO
{
    public class AttendDTO
    {
        public string DepartmentName { get; set; }
        public string EmployeeName { get; set; }
        public TimeOnly ComingTime { get; set; }
        public TimeOnly LeaveTime { get; set; }
        public DateOnly DateOfTheDay { get; set; }
        public decimal BonusForTheDay { get; set; }
        public decimal DiscountOfLatency { get; set; }
    }
}
