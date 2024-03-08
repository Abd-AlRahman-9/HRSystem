using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using HRDomain.CustomConverter;
using HRDomain.Entities;

namespace HRDomain.Entities
{
    public class Department:BaseTable,INamePropLSP
    {
        public string Name { get; set; }
        //what about doing the data type "Byte"
        public int WorkDays {  get; set; }
        // this property to indicate how the hour will be driven when the employee come late
        public decimal DeductHour { get; set; }
        public decimal BonusHour { get; set; }
        [JsonConverter(typeof(TimeCustomConvertor))]
        public TimeSpan ComingTime { get; set; } = new TimeSpan();
        [JsonConverter(typeof(TimeCustomConvertor))]
        public TimeSpan LeaveTime { get; set; }

        //>>>>>/>>>>>/>>>>>/>>>>>/>>>>>/>>>>>/>>>>>/>>>>>/>>>>>/>>>>>
        // Employee Table One T One Relation
        [ForeignKey("Manager")]
        public int? ManagerId { get; set; }
        [InverseProperty("department")]
        public Employee Manager { get; set; } // Navigational Property

        //>>>>>/>>>>>/>>>>>/>>>>>/>>>>>/>>>>>/>>>>>/>>>>>/>>>>>/>>>>>
        // Employee Table One To Many Relation
        [InverseProperty("Department")]
        public List<Employee> Employees { get; set; } = new List<Employee>(); // Navigational Property
    }
}
