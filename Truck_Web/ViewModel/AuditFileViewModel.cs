using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Truck_Web.Models;

namespace Truck_Web.ViewModel
{
    public class AuditFileViewModel
    {
        public IEnumerable<SelectListItem> TimeSheetList { get; set; }
        public AuditFile AuditFile { get; set; }
    }
}
