using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using HRDomain.CustomConverter;

namespace HRDomain.Entities
{
    public class EmployeeAttendace : BaseTable
    {
        [JsonConverter(typeof(TimeCustomConvertor))]
        public TimeOnly Attendance {  get; set; }
        [JsonConverter(typeof(TimeCustomConvertor))]
        public TimeOnly Leave { get; set; }
        [JsonConverter(typeof(DateCustomConverter))]
        public DateOnly Date { get; set; }
        public decimal Bonus { get; set; }
        public decimal Discount { get; set; }
        //FK
        public int? EmployeeId { get; set; }
        public Employee Employee { get; set; }
    }
}