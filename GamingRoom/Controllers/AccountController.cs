using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GaminRoom.Domain;
using GaminRoom.Domain.Dto;
using GaminRoom.Domain.Helpers;
using GaminRoom.Domain.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace GamingRoom.Controllers
{
    [Route("api/[controller]")]
    public class AccountController : BaseController
    {
        public AccountController(GamingRoomContext db) : base(db)
        {
        }

        // POST api/values
        [HttpPost("login")]
        public ActionResult<User> Login([FromBody] LoginDto login)
        {
            var user = _db.Users.FirstOrDefault(x => x.Username == login.Username);

            if (user == null)
                return StatusCode(500, "Invalid username/password");
            if(user.Password != HashPassword.GeneratePassword(user.Salt, login.Password))
                return StatusCode(500, "Invalid username/password");
            user.Token = RandomString.GenerateString(8);
            _db.Users.Update(user);
            _db.SaveChanges();
            return user;
        }
        [HttpPost("logout")]
        public ActionResult<User> Logout([FromBody] User user)
        {
            var userTemp = Users.FirstOrDefault(x => x.Username == user.Username);
            if (userTemp == null)
                return StatusCode(500, "Not exist");
            userTemp.Token = null;
            _db.Users.Update(user);
            _db.SaveChanges();
            return null;
        }
    }
}
