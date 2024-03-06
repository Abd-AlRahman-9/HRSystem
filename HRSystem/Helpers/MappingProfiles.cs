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
                .ForMember(D => D.ManagerName, O => O.MapFrom(M=>M.Manager.Name));
            
            // For Employee
            CreateMap<Employee, EmployeesDTO>()
                .ForMember(D => D.Manager, O => O.MapFrom(M => M.Manager.Name))
                .ForMember(D => D.Department, O => O.MapFrom(M => M.Department.Name));
            // For Settings

            // For Latency

            // For Holidays

        }
    }
}
