namespace HRSystem.DTO
{
    public class AttendDTO
    {
        public string EmpName { get; set; }
        public TimeOnly Attendance { get; set; }
        public TimeOnly Leave { get; set; }
        public DateOnly Date { get; set; }
        public decimal Bonus { get; set; }
        public decimal Discount { get; set; }
    }
}
