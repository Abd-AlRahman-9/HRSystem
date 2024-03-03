//using AutoMapper;
//using HRDomain.Entities;
//using HRSystem.DTO;

//namespace HRSystem.Helpers
//{
//    // Don't Forget to use the middle ware UseStaticFiles to make it work
//    public class EmpsPicsUrlResolver : IValueResolver<Employee, EmployeesDTO, string>
//    {
//        public IConfiguration Configuration { get; }

//        public EmpsPicsUrlResolver(IConfiguration configuration)
//        {
//            Configuration = configuration;
//        }
//        public string Resolve(Employee source, EmployeesDTO destination, string destMember, ResolutionContext context)
//        {
//            if (!string.IsNullOrEmpty(source.ImgUrl))
//                return $"{Configuration["BaseApiUrl"]}{source.ImgUrl}";

//            //Don't forget to add it ing the MappingProfiles Ctor
//            // Just add .ForMember(D=>D.ImgUrl,O=>O.MapFrom<EmpsPicsUrlResolver>());
//            return null;
//        }
//    }
//}
