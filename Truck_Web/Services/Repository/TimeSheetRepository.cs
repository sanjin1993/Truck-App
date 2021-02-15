using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Truck_Web.Models;
using Truck_Web.Services.IRepository;

namespace Truck_Web.Services.Repository
{
    public class TimeSheetRepository : Repository<TimeSheet>, ITimeSheetRepository
    {
        private readonly IHttpClientFactory _clientFactory;

        public TimeSheetRepository(IHttpClientFactory clientFactory) : base(clientFactory)
        {
            _clientFactory = clientFactory;
        }

        SqlConnection con = new SqlConnection("Data Source=localhost;Initial Catalog=TruckDB;Integrated Security=true");
        public DataTable GetRecord()
        {
            SqlCommand com = new SqlCommand("select* from TimeSheets", con);
            SqlDataAdapter da = new SqlDataAdapter(com);
            DataTable dt = new DataTable();
            da.Fill(dt);
            return dt;
        }
    }
}
