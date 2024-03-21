namespace HRSystem.Helpers
{
    public class MappingProfiles:Profile
    {
        public MappingProfiles()
        {
            // For Attendance
            CreateMap<EmployeeAttendace, AttendDTO>()
                .ForMember(DTO => DTO.EmployeeName, Opt => Opt.MapFrom(Att => Att.Employee.Name))
                .ForMember(DTO => DTO.DepartmentName, Opt => Opt.MapFrom(Att => Att.Employee.Department.Name))
                .ForMember(DTO => DTO.BonusForTheDay, Opt => Opt.MapFrom(Att => Att.Bonus))
                .ForMember(DTO => DTO.DiscountOfLatency, Opt => Opt.MapFrom(Att => Att.Discount))
                .ForMember(DTO => DTO.DateOfTheDay, Opt => Opt.MapFrom(Att => Att.Date.ToString()))
                .ForMember(DTO => DTO.ComingTime, Opt => Opt.MapFrom(Att => Att.Attendance.ToString("hh\\:mm\\:ss")))
                .ForMember(DTO => DTO.LeaveTime, Opt => Opt.MapFrom(Att => Att.Leave.ToString("hh\\:mm\\:ss")))
                .ReverseMap()
                .ForMember(Att => Att.Attendance, Opt => Opt.MapFrom(DTO => TimeSpan.Parse(DTO.ComingTime)))
                .ForMember(Att => Att.Leave, Opt => Opt.MapFrom(DTO => TimeSpan.Parse(DTO.LeaveTime)))
                .ForMember(Att => Att.Date, Opt => Opt.MapFrom(DTO => DateOnly.Parse(DTO.DateOfTheDay)));

            // For Department 
            CreateMap<Department, GetDeptsDTO>()
                .ForMember(DTO => DTO.ManagerName, Opt => Opt.MapFrom(Dept => Dept.Manager.Name))
                .ForMember(DTO => DTO.DepartmentName, Opt => Opt.MapFrom(Dept => Dept.Name))
                .ForMember(DTO => DTO.DeductionRule, Opt => Opt.MapFrom(Dept => Dept.DeductHour))
                .ForMember(DTO => DTO.BonusRule, Opt => Opt.MapFrom(Dept => Dept.BonusHour))
                .ForMember(DTO => DTO.WorkDays, Opt => Opt.MapFrom(Dept => Dept.WorkDays))
                .ForMember(DTO => DTO.TimeToLeave , Opt => Opt.MapFrom(Dept => Dept.LeaveTime.ToString("hh\\:mm\\:ss")))
                .ForMember(DTO => DTO.ComingTime, Opt => Opt.MapFrom(Dept => Dept.ComingTime.ToString("hh\\:mm\\:ss")))
                .ReverseMap()
                .ForMember(Dept => Dept.LeaveTime , Opt => Opt.MapFrom(DTO => TimeSpan.Parse(DTO.TimeToLeave)))
                .ForMember(Dept => Dept.ComingTime, Opt => Opt.MapFrom(DTO => TimeSpan.Parse(DTO.ComingTime)));

            // For Employee
            CreateMap<Employee, EmployeesDTO>()
                //.ForMember(DTO => DTO.ManagerName, Opt => Opt.MapFrom(Emp => Emp.manager.Name))
                .ForMember(DTO => DTO.Department, Opt => Opt.MapFrom(Emp => Emp.Department.Name))
                //.ForMember(dto => dto.Department, opt => opt.Ignore())
                .ForMember(DTO => DTO.EmployeeName, Opt => Opt.MapFrom(Emp => Emp.Name))
                .ForMember(DTO => DTO.Address, Opt => Opt.MapFrom(Emp => Emp.Address))
                .ForMember(DTO => DTO.Phone, Opt => Opt.MapFrom(Emp => Emp.PhoneNumber))
                .ForMember(DTO => DTO.Salary, Opt => Opt.MapFrom(Emp => Emp.Salary))
                .ForMember(DTO => DTO.Nationality, Opt => Opt.MapFrom(Emp => Emp.Nationality))
                .ForMember(DTO => DTO.Gender, Opt => Opt.MapFrom(Emp => Emp.Gender))
                .ForMember(DTO => DTO.VacationsCredit, Opt => Opt.MapFrom(Emp => Emp.VacationsRecord))
                .ForMember(DTO => DTO.DateOfBirth, Opt => Opt.MapFrom(Emp => Emp.BirthDate.ToString("dd\\-MM\\-yyyy")))
                .ForMember(DTO => DTO.HiringDate, Opt => Opt.MapFrom(Emp => Emp.HireData.ToString("dd\\-MM\\-yyyy")))
                .ReverseMap()
                .ForMember(Emp => Emp.BirthDate, Opt => Opt.MapFrom(DTO => DateOnly.Parse(DTO.DateOfBirth)))
                .ForMember(Emp => Emp.HireData, Opt => Opt.MapFrom(DTO => DateOnly.Parse(DTO.HiringDate)));

            // For Holidays
            CreateMap<Vacation, OfficialHolidaysDTO>()
                .ForMember(DTO => DTO.HolidayName, Opt => Opt.MapFrom(Hol => Hol.Name))
                .ForMember(DTO => DTO.DateOnTheCurrentYear, Opt => Opt.MapFrom(Hol => Hol.Date.ToString("dd/MM/yyyy")))
                .ReverseMap()
                .ForMember(Hol => Hol.Date, Opt => Opt.MapFrom(DTO => DateOnly.Parse(DTO.DateOnTheCurrentYear)));
        }
    }
}
