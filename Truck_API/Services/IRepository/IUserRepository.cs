using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Truck_API.Models;

namespace Truck_API.Services.IRepository
{
    public interface IUserRepository
    {
        bool IsUniqueUser(string username);
        User Autenticate(string username, string password);
        User Register(string username, string password);
    }
}
