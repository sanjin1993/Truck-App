using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Truck_API.Models;
using Truck_WebAPI.Models;

namespace Truck_WebAPI.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }

        public DbSet<TimeSheet> TimeSheets { get; set; }
        public DbSet<AuditFile> AuditFiles { get; set; }
        public DbSet<User> User { get; set; }


    }
}
