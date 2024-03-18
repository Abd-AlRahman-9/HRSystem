﻿using HRDomain.Entities;

namespace HRDomain.Specification
{
    public class CountEmpSpecification:GenericSpecification<Employee>
    {
        public CountEmpSpecification(GetAllEmpsParams getAllEmpsParams)
            : base
            (
                E =>
                    (E.Deleted==false)&&
                    (string.IsNullOrEmpty(getAllEmpsParams.Search) || E.Name.ToLower().Contains(getAllEmpsParams.Search)) &&
                    (!getAllEmpsParams.DeptId.HasValue || E.DeptId == getAllEmpsParams.DeptId.Value) &&
                    (!getAllEmpsParams.MngId.HasValue || E.ManagerId == getAllEmpsParams.MngId.Value)
            )
        { }
    }
}
