using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using static Truck_WebAPI.Models.AuditFile;

namespace Truck_WebAPI.Models.Dtos
{
    public class AuditFileDto
    {
        public int Id { get; set; }
        public string DayNo { get; set; }
        public string NewValue { get; set; }
        public DateTime DateTimeChange { get; set; }
        public Field FieldEnum { get; set; }

        public int TimeSheetId { get; set; }
        public TimeSheet TimeSheet { get; set; }
    }
}
