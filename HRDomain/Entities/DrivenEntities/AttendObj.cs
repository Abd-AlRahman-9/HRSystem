using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using HRDomain.CustomConverter;

namespace HRDomain.Entities.DrivenEntities
{
    public class AttendObj
    {
        public DateOnly Date { get; set; }
        public TimeSpan Attendance {  get; set; }
        public TimeSpan Leave { get; set; }
        public decimal Bonus { get; set; }
        public decimal Discount { get; set; }
    }
}
