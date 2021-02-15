using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Truck_API.Models;
using Truck_API.Services.IRepository;
using Truck_WebAPI.Data;

namespace Truck_API.Services.Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly ApplicationDbContext _db;
        private readonly AppSetings _appSetings;

        public UserRepository(ApplicationDbContext db, IOptions<AppSetings> appSetings)
        {
            _db = db;
            _appSetings = appSetings.Value;
        }

        public User Autenticate(string username, string password)
        {
            var user = _db.User.SingleOrDefault(x => x.Username == username && x.Password == password);

            if (user == null)
            {
                return null;
            }

            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_appSetings.Secret);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new System.Security.Claims.ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, user.Id.ToString()),
                    new Claim(ClaimTypes.Role, user.Role)
                }),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            user.Token = tokenHandler.WriteToken(token);
            user.Password = "";
            return user;

        }

        public bool IsUniqueUser(string username)
        {
            var user = _db.User.SingleOrDefault(x => x.Username == username);

            if (user == null)
            {
                return true;
            }
            return false;
        }

        public User Register(string username, string password)
        {
            User userObj = new User()
            {
                Username = username,
                Password = password,
                Role="Admin"
            };

            _db.User.Add(userObj);
            _db.SaveChanges();
            userObj.Password = "";
            return userObj;
        }
    }
}
