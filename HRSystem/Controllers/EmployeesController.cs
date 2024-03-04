using HRDomain.Specification;
using HRSystem.DTO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HRSystem.Controllers
{
    public class EmployeesController : HRBaseController
    {
        public EmployeesController()
        {
            
        }
        //public async Task<ActionResult<IEnumerable<EmployeesDTO>>> GetAllEmps(string sort,int? DeptId,int? MngId)
        //{
        //    var Specification = new EmpIncludeNavPropsSpecification (sort , DeptId , MngId);

        //}
    }
}
