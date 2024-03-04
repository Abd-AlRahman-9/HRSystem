using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HRDomain.Entities;

namespace HRDomain.Entities
{
    public class Department:BaseTable,INamePropLSP
    {
        public string Name { get; set; }
        //what about doing the data type "Byte"
        public sbyte WorkDays {  get; set; }
        // this property to indicate how the hour will be driven when the employee come late
        public double DeductHour { get; set; }
        public double BonusHour { get; set; }
        public TimeSpan ComingTime { get; set; }
        public TimeSpan LeaveTime { get; set; }

        // Foriegn Key of Employees Table
        public int? ManagerId { get; set; }
        public Employee Manager { get; set; } // Navigational Property
        public List<Employee> Employees { get; set; } = new List<Employee>(); //-> we'll handle it by Fluent API 
    }
}
