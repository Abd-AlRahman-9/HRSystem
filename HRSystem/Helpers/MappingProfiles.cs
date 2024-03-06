using AutoMapper;
using HRDomain.Entities;
using HRSystem.DTO;

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
                .ForMember(DTO => DTO.ComingTime, Opt => Opt.MapFrom(Att => Att.Attendance))
                .ForMember(DTO => DTO.LeaveTime, Opt => Opt.MapFrom(Att => Att.Leave))
                .ForMember(DTO => DTO.DateOfTheDay, Opt => Opt.MapFrom(Att => Att.Date))
                .ForMember(DTO => DTO.BonusForTheDay, Opt => Opt.MapFrom(Att => Att.Bonus))
                .ForMember(DTO => DTO.DiscountOfLatency, Opt => Opt.MapFrom(Att => Att.Discount));

            // For Department 
            CreateMap<Department, GetDeptsDTO>()
                .ForMember(D => D.ManagerName, O => O.MapFrom(M => M.Manager.Name))
                .ForMember(D => D.DepartmentName, O => O.MapFrom(D => D.Name))
                .ForMember(D => D.TimeToLeave, O => O.MapFrom(D => D.LeaveTime))
                .ForMember(D => D.ComingTime, O => O.MapFrom(D => D.ComingTime))
                .ForMember(D => D.DeductionRule, O => O.MapFrom(D => D.DeductHour))
                .ForMember(D => D.BonusRule, O => O.MapFrom(D => D.BonusHour))
                .ForMember(D => D.WorkDays, O => O.MapFrom(D => D.WorkDays));

            // For Employee
            CreateMap<Employee, EmployeesDTO>()
                .ForMember(D => D.Manager, O => O.MapFrom(M => M.Manager.Name))
                .ForMember(D => D.Department, O => O.MapFrom(M => M.Department.Name))
                .ForMember(D => D.EmployeeName, O => O.MapFrom(E => E.Name))
                .ForMember(D => D.HiringDate, O => O.MapFrom(E => E.HireData))
                .ForMember(D => D.Address, O => O.MapFrom(E => E.Address))
                .ForMember(D => D.DateOfBirth, O => O.MapFrom(E => E.BirthDate))
                .ForMember(D => D.Phone, O => O.MapFrom(E => E.PhoneNumber))
                .ForMember(D => D.Salary, O => O.MapFrom(E => E.Salary))
                .ForMember(D => D.Nationality, O => O.MapFrom(E => E.Nationality))
                .ForMember(D => D.Gender, O => O.MapFrom(E => E.Gender))
                .ForMember(D => D.VacationsCredit, O => O.MapFrom(E => E.VacationsRecord));

            // For Holidays
            CreateMap<Vacation, OfficialHolidaysDTO>()
                .ForMember(D => D.HolidayName, O => O.MapFrom(H => H.Name))
                .ForMember(D => D.DateOnTheCurrentYear, O => O.MapFrom(H => H.Date));
        }
    }
}
