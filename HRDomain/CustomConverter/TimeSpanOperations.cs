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

            if ((regex.IsMatch(arrive) && !regex.IsMatch(leave)))
                return true;
            return false;
        }
       
    }
}
