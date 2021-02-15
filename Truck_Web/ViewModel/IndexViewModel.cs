using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Truck_Web.Models;

namespace Truck_Web.ViewModel
{
    public class IndexViewModel
    {
        public IEnumerable<TimeSheet> TimeSheetList { get; set; }
        public IEnumerable<AuditFile> AuditFileList { get; set; }
    }
}
