using AutoMapper;
using HRDomain.Entities;
using HRSystem.DTO;

namespace HRSystem.Helpers
{
    public class MappingProfiles:Profile
    {
        public MappingProfiles()
        {
            CreateMap<Department, GetDeptsDTO>()
                .ForMember(D => D.ManagerName, O => O.MapFrom(M=>M.Manager.Name));
            // For each DTO have a Navigation Property and you don't need to send the whole object
            CreateMap<Employee, EmployeesDTO>()
                .ForMember(D => D.Manager, O => O.MapFrom(M => M.Manager.Name))
                .ForMember(D => D.Department, O => O.MapFrom(M => M.Department.Name));
        }
    }
}
