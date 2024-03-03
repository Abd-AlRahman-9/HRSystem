using AutoMapper;
using HRDomain.Entities;

namespace HRSystem.Helpers
{
    public class MappingProfiles:Profile
    {
        public MappingProfiles()
        {
            //For each DTO have a Navigation Property and you don't need to send the whole object
            //CreateMap<Employee,EmployeeDTO>()
            //    .ForMember(D=>D.Manger,O=>O.MapFrom(M=>M.Manager.Name));
        }
    }
}
