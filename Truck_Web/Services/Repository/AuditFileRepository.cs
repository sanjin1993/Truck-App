using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Truck_Web.Models;
using Truck_Web.Services.IRepository;

namespace Truck_Web.Services.Repository
{
    public class AuditFileRepository : Repository<AuditFile>, IAuditFileRepository
    {
        private readonly IHttpClientFactory _clientFactroy;
        public AuditFileRepository(IHttpClientFactory clientFactory) : base(clientFactory)
        {
            _clientFactroy = clientFactory;
        }
    }
}
