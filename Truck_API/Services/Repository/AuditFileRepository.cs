using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Truck_WebAPI.Data;
using Truck_WebAPI.Models;
using Truck_WebAPI.Services.IRepository;

namespace Truck_WebAPI.Services.Repository
{
    public class AuditFileRepository : IAuditFileRepository
    {
        private readonly ApplicationDbContext _db;
        public AuditFileRepository(ApplicationDbContext db)
        {
            _db = db;
        }

        public bool AuditFileExists(string dayNo)
        {
            bool value = _db.AuditFiles.Any(a => a.DayNo.ToLower().Trim() == dayNo.ToLower().Trim());
            return value;
        }

        public bool AuditFileExists(int id)
        {
            return _db.AuditFiles.Any(a => a.Id == id);
        }

        public bool CreateAuditFile(AuditFile auditFile)
        {
            _db.AuditFiles.Add(auditFile);
            return Save();
        }

        public bool DeleteAuditFile(AuditFile auditFile)
        {
            _db.AuditFiles.Remove(auditFile);
            return Save();
        }

        public AuditFile GetAuditFile(int auditFileId)
        {
            return _db.AuditFiles.Include(a => a.TimeSheet).FirstOrDefault(a => a.Id == auditFileId);
        }

        public ICollection<AuditFile> GetAuditFiles()
        {
            return _db.AuditFiles.Include(a => a.TimeSheet).OrderBy(a => a.DayNo).ToList();
        }

        public ICollection<AuditFile> GetAuditFilesInTimeSheet(int timeSheetId)
        {
            return _db.AuditFiles.Include(a => a.TimeSheet).Where(c => c.TimeSheetId == timeSheetId).ToList();
        }

        public bool Save()
        {
            return _db.SaveChanges() >= 0 ? true : false;
        }

        public bool UpdateAuditFile(AuditFile auditFile)
        {
            _db.AuditFiles.Update(auditFile);
            return Save();
        }
    }
}
