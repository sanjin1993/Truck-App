using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Truck_Web.Models
{
    public class AuditFile
    {
        public int Id { get; set; }
        public string DayNo { get; set; }
        public string NewValue { get; set; }
        public DateTime DateTimeChange { get; set; }
        public enum Field { Start, Break, End }
        public Field FieldEnum { get; set; }

        public int TimeSheetId { get; set; }
        [ForeignKey("TimeSheetId")]
        public TimeSheet TimeSheet { get; set; }
    }
}
