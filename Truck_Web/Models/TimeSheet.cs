using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Truck_Web.Models
{
    public class TimeSheet
    {
        public int Id { get; set; }
        public string Type { get; set; }
        public string StartTime { get; set; }
        public string EndTime { get; set; }
        public string Break { get; set; }
        public string M { get; set; }
        public string KmStand { get; set; }
        public string Privat { get; set; }
        public string Fuel { get; set; }
        public string AdBlue { get; set; }
        public string Notes { get; set; }
    }
}
