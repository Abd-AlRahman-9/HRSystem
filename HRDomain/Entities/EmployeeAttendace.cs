using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using HRDomain.CustomConverter;

namespace HRDomain.Entities
{
    public class EmployeeAttendace : BaseTable
    {
        [JsonConverter(typeof(TimeCustomConvertor))]
        public TimeSpan Attendance {  get; set; }
        [JsonConverter(typeof(TimeCustomConvertor))]
        public TimeSpan Leave { get; set; }
        [JsonConverter(typeof(DateCustomConverter))]
        public DateOnly Date { get; set; }
        public decimal Bonus { get; set; }
        public decimal Discount { get; set; }

        ///>>>>>/>>>>>/>>>>>/>>>>>/>>>>>/>>>>>/>>>>>/>>>>>/>>>>>/>>>>>
        // Employee Table One To Many relation
        [ForeignKey("Employee")]
        public int? EmployeeId { get; set; }
        [InverseProperty("employeeAttendaces")]
        public Employee Employee { get; set; }
    }
}