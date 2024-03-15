﻿using AutoMapper;
using HRDomain.CustomConverter;
using HRDomain.Entities;
using HRDomain.Specification;
using HRRepository;
using HRSystem.DTO;
using HRSystem.Error_Handling;
using HRSystem.Helpers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Storage.Internal;
using Microsoft.IdentityModel.Tokens;
using Microsoft.SqlServer.Server;
using System;
using System.Globalization;
using System.Linq.Expressions;
using System.Text.RegularExpressions;

namespace HRSystem.Controllers
{
    public class DepartmentsController : HRBaseController
    {
        private readonly GenericRepository<Department> _DeptRepo;
        private readonly GenericRepository<Employee> _EmpRepo;
        private readonly IMapper mapper;
        private readonly IConfiguration _configuration;
        readonly ADOProcedures aDOProcedures;


        public DepartmentsController(GenericRepository<Department> repository,GenericRepository<Employee> EmpRepo, IMapper mapper, IConfiguration configuration)
        {
            this._DeptRepo = repository;
            this._EmpRepo = EmpRepo;
            this.mapper = mapper;
            _configuration = configuration;
            string connectionString = _configuration.GetConnectionString("Default");
        aDOProcedures = new ADOProcedures(connectionString);
        }
        [HttpGet("{Name}", Name = "GetDepartmentByName")]
        public async Task<ActionResult<GetDeptsDTO>> GetOneDept (string Name)
        {
            var specification = new DeptIncludeNavPropsSpecification(Name);
            var Dept = await _DeptRepo.GetSpecified(specification);
            if (Dept is null) return NotFound(new ErrorResponse(404));
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

        [HttpPost ("WorkDays")]
        public async Task<ActionResult> Create(GetDeptsDTO deptsDTO, int workDays)
        {
            if (!TimeSpanOperations.IsTime(deptsDTO.ComingTime, deptsDTO.TimeToLeave))
                return BadRequest("Invalid time format,Please provide the time in the format 'hh:mm:ss'");

            deptsDTO.WorkDays = (sbyte)workDays;
            var department = mapper.Map<Department>(deptsDTO);
            // var mangers= aDOProcedures.GetManagers();
            department.Manager = null;
            
            await _DeptRepo.AddAsync(department);
            string uri = Url.Action(nameof(GetOneDept), new { id = department.Id });

            return Created(uri, "Created succsessfully");
        }

        [HttpPut("Edit/{name}")]
        public async Task<ActionResult> Edit(string name,GetDeptsDTO deptsDTO)
        {
            var specification = new DeptIncludeNavPropsSpecification(name);
            var department = await _DeptRepo.GetSpecified(specification);
            if (department is null) return NotFound(new ErrorResponse(404,$"Uneable to find {name} department"));

            if (!TimeSpanOperations.IsTime(deptsDTO.ComingTime, deptsDTO.TimeToLeave))
                return BadRequest("Invalid time format,Please provide the time in the format '00:00:00'");
            var dept= mapper.Map<Department>(deptsDTO);
            var manger = new EmpIncludeNavPropsSpecification(deptsDTO.ManagerName, department.Id);
            Employee departmentManger= await _EmpRepo.GetSpecified(manger);
            if(departmentManger is null) return NotFound(new ErrorResponse(404,"Manger Name can't be found."));
            dept.Manager = departmentManger;
            dept.Manager.Department = null;
            dept.Manager.DeptId = null;

            Expression<Func<Department, bool>> predicate = d => d.Name == name;
            await _DeptRepo.UpdateAsync(predicate,name,dept);
            return StatusCode(202);
        }

        [HttpDelete("delete/{name}")]
        public async Task<ActionResult> SoftDelete(string name)
        {
            var specification = new DeptIncludeNavPropsSpecification(name);
            var department = await _DeptRepo.GetSpecified(specification);
            if (department is null) return NotFound(new ErrorResponse(404, $"Uneable to find {name} department"));
            Expression<Func<Department, bool>> predicate= d => d.Name == name;
               await _DeptRepo.DeleteAsync( predicate, name);

                return StatusCode(200, "Deleted Succsessfully");
        }
    }
}
