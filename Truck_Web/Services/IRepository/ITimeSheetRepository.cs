using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Truck_Web.Models;

namespace Truck_Web.Services.IRepository
{
    public interface ITimeSheetRepository : IRepository<TimeSheet>
    {
        DataTable GetRecord();
    }
}
