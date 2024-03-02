using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace HRDomain.Entities
{
    //public enum Gender
    //{
    //    Male=0, male=0, M = 0, m =0,
    //    Female=1, female=1, F=1, f=1,
    //    Unknown=2, unknown=2
    //}
    public class Employee:BaseTables,INamePropLSP
    {
        public string Name { get; set; }
        // search about the best practice of the national id prop
        // Done it was between {"string", "long"}
        public string NationalID { get; set; }
        public DateOnly BirthDate { get; set; }
        public string Nationality { get; set; }
        public string Address { get; set; }
        // search about the best practices on making the gender
        // Done and I decided to Make it Enum in the DTO layer 
        public string Gender { get; set; }
        public long PhoneNumber { get; set; }
        public int VacationsRecord { get; set; }
        public decimal Salary { get; set; }
        public DateOnly HireData { get; set; }
        // Foreign Key Of Self Relationship
        public int? ManagerId { get; set; }
        // Foreign Key Of the Department Table
        public int? DeptId { get; set; }
        public Department Department { get; set; } // Navigational Property
        public Employee Manager { get; set; } // Navigational Property
        public List<EmployeeVacation> EmployeeVacations { get; set; } = new List<EmployeeVacation>();
    }
}
