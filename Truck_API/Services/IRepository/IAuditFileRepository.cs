using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Truck_WebAPI.Models;

namespace Truck_WebAPI.Services.IRepository
{
    public interface IAuditFileRepository
    {
        ICollection<AuditFile> GetAuditFiles();
        ICollection<AuditFile> GetAuditFilesInTimeSheet(int timeSheetId);

        AuditFile GetAuditFile(int auditFileId);
        bool AuditFileExists(string dayNo);
        bool AuditFileExists(int id);
        bool CreateAuditFile(AuditFile auditFile);
        bool UpdateAuditFile(AuditFile auditFile);
        bool DeleteAuditFile(AuditFile auditFile);
        bool Save();
    }
}
