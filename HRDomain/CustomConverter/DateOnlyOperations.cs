using System.Globalization;

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
            throw new Exception(message: $"{date} is invalid date format. Please provide the date in the format 'dd-mm-yyyy'", innerException : null);
        }

        public static bool IsValidDate(int year)
        {
            if (year >= DateTime.Now.Year)
           return true;

            return false;


        }
    }
}
