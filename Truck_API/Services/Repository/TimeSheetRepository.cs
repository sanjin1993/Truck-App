using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Truck_WebAPI.Data;
using Truck_WebAPI.Models;
using Truck_WebAPI.Services.IRepository;

namespace Truck_WebAPI.Services.Repository
{
    public class TimeSheetRepository : ITimeSheetRepository
    {
        private readonly ApplicationDbContext _db;

        public TimeSheetRepository(ApplicationDbContext db)
        {
            _db = db;
        }

        public bool CreateTimeSheet(TimeSheet timeSheet)
        {
            _db.TimeSheets.Add(timeSheet);
            return Save();
        }

        public bool DeleteTimeSheet(TimeSheet timeSheet)
        {
            _db.TimeSheets.Remove(timeSheet);
            return Save();
        }

        public TimeSheet GetTimeSheet(int timeSheetId)
        {
            return _db.TimeSheets.FirstOrDefault(a => a.Id == timeSheetId);
        }

        public ICollection<TimeSheet> GetTimeSheets()
        {
            return _db.TimeSheets.OrderBy(a => a.Type).ToList();
        }

        public bool Save()
        {
            return _db.SaveChanges() >= 0 ? true : false;
        }

        public bool TimeSheetExists(string type)
        {
            bool value = _db.TimeSheets.Any(a => a.Type.ToLower().Trim() == type.ToLower().Trim());
            return value;
        }

        public bool TimeSheetExists(int id)
        {
            return _db.TimeSheets.Any(a => a.Id == id);
        }

        public bool UpdateTimeSheet(TimeSheet timeSheet)
        {
            _db.TimeSheets.Update(timeSheet);
            return Save();
        }
    }
}
