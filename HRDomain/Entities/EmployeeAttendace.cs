using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRDomain.Entities
{
    public class EmployeeAttendace : BaseTable
    {
        public TimeOnly Attendance {  get; set; }
        public TimeOnly Leave { get; set; }
        public DateOnly Date { get; set; }
        public decimal Bonus { get; set; }
        public decimal Discount { get; set; }
        //FK
        public int? EmployeeId { get; set; }
        public Employee Employee { get; set; }
    }
}