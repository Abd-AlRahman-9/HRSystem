using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using HRDomain.CustomConverter;

namespace HRDomain.Entities
{
    public class Vacation : BaseTable,INamePropLSP
    {
        public string Name { get; set; }
        // this bool field indicates this day was a official holiday (Will not Decrease the salary)
        public bool Holiday {  get; set; }
        [JsonConverter(typeof(DateCustomConverter))]
        public DateOnly Date {  get; set; }
        public List<EmployeeVacation> EmployeeVacations { get; set; } = new List<EmployeeVacation>();
    }
}
