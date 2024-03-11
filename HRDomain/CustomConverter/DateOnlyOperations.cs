using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRDomain.CustomConverter
{
    public class DateOnlyOperations
    {
        public static DateOnly ToDateOnly(string date)
        {

                if (DateTime.TryParseExact(date, "dd-MM-yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime parsedDate))
                {
                    var dateOnly = DateOnly.FromDateTime(parsedDate);
                    return dateOnly;
                }
           throw new InvalidDataException("Invalid date format. Please provide the date in the format 'dd-mm-yyyy'");


        }

        public static bool IsValidDate(int year, int month, int day)
        {
            if (year >= DateTime.Now.Year && year <=2999 && month >= DateTime.Now.Month && day > DateTime.Now.Day)
           return true;

            return false;


        }
    }
}
