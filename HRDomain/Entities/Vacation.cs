using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using HRDomain.CustomConverter;

namespace HRDomain.Entities
{
    public class Vacation : BaseTable,INamePropLSP
    {
        public string Name { get; set; }
        // this bool field indicates this day was a official holiday (Will not Decrease the salary)
        public bool Holiday { get; set; } = true;
        [JsonConverter(typeof(DateCustomConverter))]
        public DateOnly Date {  get; set; }

        //>>>>>/>>>>>/>>>>>/>>>>>/>>>>>/>>>>>/>>>>>/>>>>>/>>>>>/>>>>>
        // EmployeeVacation One To Many Relation
        [InverseProperty("Vacation")]
        public List<EmployeeVacation> EmployeesVacation { get; set; } = new List<EmployeeVacation>();
    }
}
