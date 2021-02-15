using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Truck_API.Models
{
    public class User
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string Name { get; set; }
        public int FM { get; set; }
        public string Role { get; set; }
        public DateTime EntryDate { get; set; }
        public string Email { get; set; }
        [NotMapped]
        public string Token { get; set; }
    }
}
