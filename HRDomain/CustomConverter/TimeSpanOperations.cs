using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace HRDomain.CustomConverter
{
    public class TimeSpanOperations
    {
        public static bool IsTime(string arrive ,string leave)
        {
            string pattern = @"^([0-1][0-9]|2[0-3]):([0-5][0-9]):([0-5][0-9])$";
            Regex regex = new Regex(pattern);

            if ((regex.IsMatch(arrive) && regex.IsMatch(leave)))
                return true;
            return false;
        }
        public static decimal CalculateBonusHours (TimeSpan departmentAttend, TimeSpan employeeAttend, TimeSpan departmentLeave, TimeSpan employeeLeave)
        {
            double bonus = 0; 

            if (departmentAttend > employeeAttend)
            {
                var attend = departmentAttend - employeeAttend;   
                
                bonus += (attend.Hours) +(attend.Minutes /60.00);
            }
            if (departmentLeave < employeeLeave)
            {
                var leave = employeeLeave - departmentLeave;
                bonus += (leave.Hours) + (leave.Minutes / 60.00);
            }
            return (decimal)bonus;
        }
        public static decimal CalculateDiscountHours(TimeSpan departmentAttend, TimeSpan employeeAttend, TimeSpan departmentLeave, TimeSpan employeeLeave)
        {
            double discount = 0.00;
            if (departmentAttend < employeeAttend)
            {
                var attend = employeeAttend - departmentAttend;
                discount += (attend.Hours) + (attend.Minutes / 60.00); 
            }
            if (departmentLeave > employeeLeave)
            {
                var leave = departmentLeave - employeeLeave;
                discount += (leave.Hours) + (leave.Minutes / 60.00);
            }
            return (decimal)discount;
        }


    }
}
