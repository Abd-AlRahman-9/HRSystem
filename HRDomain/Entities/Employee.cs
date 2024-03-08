using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using HRDomain.CustomConverter;
namespace HRDomain.Entities
{
    //public enum Gender
    //{
    //    Male=0, male=0, M = 0, m =0,
    //    Female=1, female=1, F=1, f=1,
    //    Unknown=2, unknown=2
    //}
    public class Employee:BaseTable,INamePropLSP
    {
        public string Name { get; set; }
        // search about the best practice of the national id prop
        // Done it was between {"string", "long"}
        public string NationalID { get; set; }
        [JsonConverter(typeof(DateCustomConverter))]
        public DateOnly BirthDate { get; set; }
        public string Nationality { get; set; }
        public string Address { get; set; }
        // search about the best practices on making the gender
        // Done and I decided to Make it Enum in the DTO layer 
        public string Gender { get; set; }
        //???????????????phone must be string
        public string PhoneNumber { get; set; }
        public int VacationsRecord { get; set; }
        public decimal Salary { get; set; }
        [JsonConverter(typeof(DateCustomConverter))]
        public DateOnly HireData { get; set; }
        // this table has many relations and was complex to resolve
        //>>>>>/>>>>>/>>>>>/>>>>>/>>>>>/>>>>>/>>>>>/>>>>>/>>>>>/>>>>>
        // Self Relationship
        [ForeignKey("manager")]
        public int? ManagerId { get; set; }
        [InverseProperty("employees")]
        public Employee manager { get; set; } // Navigational Property
        [InverseProperty("manager")]
        public List<Employee> employees { get; set;} // Navgational Property
        //>>>>>/>>>>>/>>>>>/>>>>>/>>>>>/>>>>>/>>>>>/>>>>>/>>>>>/>>>>>
        // Department Table One To Many
        [ForeignKey("Department")]
        public int? DeptId { get; set; }
        [InverseProperty("Employees")]
        public Department Department { get; set; } // Navigational Property

        //>>>>>/>>>>>/>>>>>/>>>>>/>>>>>/>>>>>/>>>>>/>>>>>/>>>>>/>>>>>
        // Department Table One To One
        [InverseProperty("Manager")]
        public Department department { get; set; } //Navigational Property

        //>>>>>/>>>>>/>>>>>/>>>>>/>>>>>/>>>>>/>>>>>/>>>>>/>>>>>/>>>>>
        // EmployeeVacation Table One To Many Relation
        [InverseProperty("Employee")]
        public List<EmployeeVacation> EmployeeVacations { get; set; } = new List<EmployeeVacation>();

        //>>>>>/>>>>>/>>>>>/>>>>>/>>>>>/>>>>>/>>>>>/>>>>>/>>>>>/>>>>>
        // EmployeeAttendace Table One To Many Relation
        [InverseProperty("Employee")]
        public List<EmployeeAttendace> employeeAttendaces { get; set; }= new List<EmployeeAttendace>();
    }
}
