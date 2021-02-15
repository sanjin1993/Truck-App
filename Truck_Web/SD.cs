using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Truck_Web
{
    public static class SD
    {
        public static string APIBaseUrl = "https://localhost:44373/";
        public static string TimeSheetAPIPath = APIBaseUrl + "api/v1/timeSheets/";
        public static string AuditFilePath = APIBaseUrl + "api/v1/auditFiles/";
        public static string AccountPath = APIBaseUrl + "api/v1/User/";


    }
}
