using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Truck_WebAPI.Models;

namespace Truck_WebAPI.Services.IRepository
{
    public interface ITimeSheetRepository
    {
        ICollection<TimeSheet> GetTimeSheets();
        TimeSheet GetTimeSheet(int timeSheetId);
        bool TimeSheetExists(string type);
        bool TimeSheetExists(int id);
        bool CreateTimeSheet(TimeSheet timeSheet);
        bool UpdateTimeSheet(TimeSheet timeSheet);
        bool DeleteTimeSheet(TimeSheet timeSheet);
        bool Save();
    }
}
