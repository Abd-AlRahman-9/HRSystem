﻿using System.Linq.Expressions;
using HRDomain.Entities;
using HRDomain.Specification.Params;

namespace HRDomain.Specification.EntitiesSpecification
{
    public class AttendIncludeNavPropsSpecification : GenericSpecification<EmployeeAttendace>
    {
        public AttendIncludeNavPropsSpecification(GetAllAttendancesParams P) : base
            (
                A =>
                    A.Deleted == false
                    &&
                    (
                        !(P.To.HasValue) ||
                        (A.Date >= new DateOnly(P.Year, P.From, 1)) &&
                        (A.Date <= new DateOnly(P.Year, P.To.Value, DateTime.DaysInMonth(P.Year, P.To.Value)))
                    )
                    &&
                    ((P.To.HasValue) || (A.Date >= new DateOnly(P.Year, P.From, 1)))
                    //&&
                    //(
                    //    string.IsNullOrEmpty(P.Search) ||
                    //    (A.Employee.Name.ToLower().Contains($"{P.Search}".ToLower()) ||
                    //    A.Employee.Department.Name.ToLower().Contains($"{P.Search}".ToLower()))
                    //)
            )
        {
            Includes.Add(A => A.Employee);
            Includes.Add(A => A.Employee.Department);

            //Expression<Func<EmployeeAttendace, bool>> newCondition;
            //newCondition = A => (
            //                        string.IsNullOrEmpty(P.Search) ||
            //                        (A.Employee.Name.ToLower().Contains($"{P.Search}".ToLower()) ||
            //                        A.Employee.Department.Name.ToLower().Contains($"{P.Search}".ToLower()))
            //                    );
            //var parameter = Expression.Parameter(typeof(EmployeeAttendace));
            //var body = Expression.AndAlso(Criteria.Body, 
            //    Expression.Invoke(newCondition,parameter));
            //Criteria = Expression.Lambda<Func<EmployeeAttendace, bool>>(body, Criteria.Parameters.Single());

            if (P == null)
                P = new GetAllAttendancesParams() { PageSize = 10, PageIndex = 1 };
            if(P.Search==null)
            ApplyPagination(P.PageSize * (P.PageIndex - 1), P.PageSize);


            if (!string.IsNullOrEmpty(P.sort))
            {
                switch (P.sort)
                {
                    case "DateAsc":
                        AddOrderBy(A => A.Date);
                        break;
                    case "DateDesc":
                        AddOrderByDescending(A => A.Date);
                        break;
                    default:
                        AddOrderByDescending(A => A.Discount);
                        break;
                }
            }
        }
        public AttendIncludeNavPropsSpecification(string EmpName, DateOnly date) : base(A => A.Deleted == false && A.Date == date && A.Employee.Name == EmpName)
        {
            Includes.Add(A => A.Employee);
            Includes.Add(A => A.Employee.Department);
            //ThenIncludes.Add(A => A.Employee.Department);
        }
    }
}
