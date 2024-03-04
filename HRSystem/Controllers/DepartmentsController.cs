using HRDomain.Entities;
using HRDomain.Specification;
using HRRepository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HRSystem.Controllers
{
    public class DepartmentsController : HRBaseController
    {
        private readonly DepartmentRepository _DeptRepo;

        public DepartmentsController(DepartmentRepository repository)
        {
            this._DeptRepo = repository;
        }
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Department>>> GetAllDepts() 
        {
            var Depts = await _DeptRepo.GetAllAsync();
            return Ok(Depts);
        }

    }
}
