using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRDomain.Entities
{
    // this Entity Comes From A relation Many To Many between Employees table and Vacation Table
    public class EmployeeVacation : BaseTables
    {
        // Here Was A inherited Prop "Id" :it Stands for the Employee Id Deal with it as a Foriegn Key and also Part form Compost PK
        // ^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^
        // Foriegn Key From Vacations Table and the Second Part of PK
        public int? EmployeeId { get; set; }
        public int? VacationId { get; set; }
        public Vacation Vacation { get; set; } // Navigational Property
        public Employee Employee { get; set; } // Navigational Property
    }
}
