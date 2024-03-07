using AutoMapper;
using HRDomain.Entities;
using HRDomain.Specification;
using HRRepository;
using HRSystem.DTO;
using HRSystem.Helpers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.SqlServer.Server;
using System.Globalization;

namespace HRSystem.Controllers
{
    public class DepartmentsController : HRBaseController
    {
        private readonly GenericRepository<Department> _DeptRepo;
        private readonly GenericRepository<Employee> _EmpRepo;
        private readonly IMapper mapper;

        public DepartmentsController(GenericRepository<Department> repository,GenericRepository<Employee> EmpRepo, IMapper mapper)
        {
            this._DeptRepo = repository;
            this._EmpRepo = EmpRepo;
            this.mapper = mapper;
        }
        [HttpGet("{Name:alpha}", Name = "GetDepartmentByName")]
        public async Task<ActionResult<GetDeptsDTO>> GetOneDept (string Name)
        {
            var specification = new DeptIncludeNavPropsSpecification(Name);
            var Dept = await _DeptRepo.GetSpecified(specification);
            return Ok(mapper.Map<Department,GetDeptsDTO>(Dept));
        }
        [HttpGet]
        public async Task<ActionResult<Pagination<GetDeptsDTO>>> GetAllDepts([FromQuery]GetAllDeptsParams P) 
        {
            if (P.MngNationalId != null)
            {
                var Mng = await _EmpRepo.GetSpecified(new EmpIncludeNavPropsSpecification(P.MngNationalId));
                P.MngId = Mng.Id;
            }
            var specification = new DeptIncludeNavPropsSpecification(P);
            var Depts = await _DeptRepo.GetAllWithSpecificationsAsync(specification);
            var Data = mapper.Map<IEnumerable<Department>, IEnumerable<GetDeptsDTO>>(Depts);
            var countSpec = new CountDeptSpecification(P);
            var count = await _DeptRepo.GetCountAsync(countSpec);
            return Ok(new Pagination<GetDeptsDTO>(P.PageIndex,P.PageSize,count,Data));
        }
        //[HttpGet("{id:int}", Name = ("GetDepartmentById"))]
        //public async Task<ActionResult> GetById(int id)
        //{
        //    var department = await _DeptRepo.GetByIdAsync(id);
        //    if(department is null)
        //    {
        //        return BadRequest("Not Found!");
        //    }
        //    GetDeptsDTO deptsDTO = new();
        //    deptsDTO = mapper.Map<GetDeptsDTO>(department);
        //    return Ok(deptsDTO);
        //}
        [HttpPost]
        public async Task<ActionResult> Create(GetDeptsDTO deptsDTO)
        {
            var department = new Department
            {
                Name = deptsDTO.DepartmentName,
                LeaveTime = TimeSpan.Parse(deptsDTO.TimeToLeave),
                ComingTime = TimeSpan.Parse(deptsDTO.ComingTime),
                DeductHour = deptsDTO.DeductHour,
                BonusHour = deptsDTO.BonusHour,
                WorkDays = deptsDTO.WorkDays
            };
           await _DeptRepo.AddAsync(department);
            string uri = Url.Action(nameof(GetOneDept), new { id = department.Id });

         return Created(uri, "Created succsessfully");


            #region
            //if (ModelState.IsValid)
            //{
            //    try
            //    {
            //        string inputTime = deptsDTO.ComingTime;

            //        string[] timeParts = inputTime.Split(':');
            //        if (timeParts.Length == 3)
            //        {
            //            if (int.TryParse(timeParts[0], out int hour) &&
            //                int.TryParse(timeParts[1], out int min) &&
            //                int.TryParse(timeParts[2], out int sec))
            //            {
            //                TimeSpan comingTime = new TimeSpan(hour, min, sec);
            //                var department = mapper.Map<Department>(deptsDTO);

            //                await _DeptRepo.AddAsync(department);
            //                string uri = Url.Action(nameof(GetOneDept), new { id = department.Id });

            //                return Created(uri, "Created succsessfully");
            //            }

            //        }
            //         return BadRequest("Time does not match the expected format (HH:mm:ss)."); 

            //    }
            //    catch (Exception ex)
            //    {

            //        throw;
            //    }
            //}
            //return BadRequest();
            #endregion
        }

        [HttpPut("{name:alpha}")]
        public async Task<ActionResult> Edit(string name,GetDeptsDTO deptsDTO)
        {
            var specification = new DeptIncludeNavPropsSpecification(name);
            var department = await _DeptRepo.GetSpecified(specification);
            if (department is null)
            {
                return NotFound();
            }
           var dept= mapper.Map<Department>(deptsDTO);
           await _DeptRepo.UpdateAsync(name, dept);
            return StatusCode(202);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> SoftDelete(int id)
        {
            var department = _DeptRepo.GetByIdAsync(id);
            if (department is not null)
            {
              await _DeptRepo.DeleteAsync(id);
                return StatusCode(200, "Deleted Succsessfully");
            }
            return NotFound("Element isn't exist");
        }
    }
}
