using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRDomain.Entities.DrivenEntities
{
    public class SalaryObj
    {
        public string EmployeeName { get; set; }
        public string NationalID { get; set; }
        public string DepartmentName { get; set; }
        public decimal BasicSalary { get; set; }
        public int AbsenceDays { get; set; }
        public int AttendDays { get; set; }
        public decimal OverallBonusHours { get; set; }
        public decimal OverallDiscountHours { get; set; }
        public decimal OverallDiscount { get; set; }
        public decimal OverallBonus { get; set; }
        public decimal NetSalary { get; set; }
    }
}
