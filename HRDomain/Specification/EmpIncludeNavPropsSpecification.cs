﻿using HRDomain.Entities;

namespace HRDomain.Specification
{
    public class EmpIncludeNavPropsSpecification:GenericSpecification<Employee>
    {
        public EmpIncludeNavPropsSpecification(GetAllEmpsParams getAllEmpsParams)
            :base
            (
                E =>
                    (E.Deleted==false)&&
                    (string.IsNullOrEmpty(getAllEmpsParams.Search) || E.Name.ToLower().Contains(getAllEmpsParams.Search)) &&
                    (!getAllEmpsParams.DeptId.HasValue || E.DeptId == getAllEmpsParams.DeptId.Value) &&
                    (!getAllEmpsParams.MngId.HasValue || E.ManagerId == getAllEmpsParams.MngId.Value)
            )
        {
            Includes.Add(E=>E.Department);
            Includes.Add(E => E.manager);

            ApplyPagination(getAllEmpsParams.PageSize.Value * (getAllEmpsParams.PageIndex.Value - 1), getAllEmpsParams.PageSize.Value);

            if (!string.IsNullOrEmpty(getAllEmpsParams.sort))
            {
                switch (getAllEmpsParams.sort)
                {
                    case "hiringAsc":
                        AddOrderBy(E => E.HireDate); 
                        break;
                    case "hiringDesc":
                        AddOrderByDescending(E => E.HireDate);
                        break;
                    case "salaryAsc":
                        AddOrderBy(E=>E.Salary);
                        break;
                    case "salaryDesc":
                        AddOrderByDescending(E => E.Salary);
                        break;
                    default:
                        AddOrderBy(E => E.Name);
                        break;
                }
            }
        }
        public EmpIncludeNavPropsSpecification(string NationalId):base(E=>(E.NationalID == NationalId)&&(E.Deleted==false))
        {
            Includes.Add(E => E.Department);
            Includes.Add(E => E.manager);
        }

        public EmpIncludeNavPropsSpecification(string name,int id) : base(E => (E.Name == name) && (E.Deleted == false))
        {
            Includes.Add(E => E.Department);
            Includes.Add(E => E.manager);
            
        }
    }
}
