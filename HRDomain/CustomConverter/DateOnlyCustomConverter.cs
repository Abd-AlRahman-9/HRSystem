using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRDomain.CustomConverter
{
    public class DateOnlyCustomConverter
    {
        public static DateOnly ToDateOnly(string date)
        {

            try
            {
                if (DateTime.TryParseExact(date, "dd-MM-yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime parsedDate))
                {
                    var dateOnly = DateOnly.FromDateTime(parsedDate);
                    return dateOnly;
                }
            }
            catch (Exception ex)
            {
            }
            throw new Exception("Invalid date format. Please provide the date in the format 'dd-mm-yyyy'");
        }
    }
}
