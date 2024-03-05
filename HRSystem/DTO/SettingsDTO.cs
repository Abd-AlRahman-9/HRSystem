namespace HRSystem.DTO
{
    public class SettingsDTO
    {
        public int Id { get; set; }
        public bool Deleted { get; set; }
        public string Name { get; set; }
        public sbyte WorkDays { get; set; }
        public double DeductHour { get; set; }
        public double BonusHour { get; set; }
        public TimeSpan ComingTime { get; set; }
        public TimeSpan LeaveTime { get; set; }
    }
}
