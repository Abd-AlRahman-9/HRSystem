using System.Text.RegularExpressions;

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
        public static int Compare(string arrive, string leave)
        {
            if (!IsTime(arrive, leave)) return 0;
            var differance = TimeSpan.Parse(arrive).Hours - TimeSpan.Parse(leave).Hours;
            return differance;
        }
        public static decimal CalculateBonusHours (TimeSpan departmentAttend, string employeeAttend, TimeSpan departmentLeave, string employeeLeave)
        {
            double bonus = 0.0;

            TimeSpan timeToCome = TimeSpan.Parse(employeeAttend);
            TimeSpan timeToLeave = TimeSpan.Parse(employeeLeave) + TimeSpan.FromHours(12);

            if (departmentAttend > timeToCome)
            {
                var attend = departmentAttend - timeToCome;   
                
                bonus += (attend.Hours) +(attend.Minutes /60.00);
            }
            if (departmentLeave < timeToLeave)
            {
                var leave = timeToLeave - departmentLeave;
                bonus += (leave.Hours) + (leave.Minutes / 60.00);
            }
            return (decimal)bonus;
        }

        public static decimal CalculateDiscountHours(TimeSpan departmentAttend, string employeeAttend, TimeSpan departmentLeave, string employeeLeave)
        {
            double discount = 0.00;

            TimeSpan timeToCome = TimeSpan.Parse(employeeAttend);
            TimeSpan timeToLeave = TimeSpan.Parse(employeeLeave) + TimeSpan.FromHours(12);

            if (departmentAttend < timeToCome)
            {
                var attend = timeToCome - departmentAttend;
                discount += (attend.Hours) + (attend.Minutes / 60.00); 
            }
            if (departmentLeave > timeToLeave)
            {
                var leave = departmentLeave - timeToLeave;
                discount += (leave.Hours) + (leave.Minutes / 60.00);
            }
            return (decimal)discount;
        }


    }
}
