using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GaminRoom.Domain;
using GaminRoom.Domain.Dto;
using GaminRoom.Domain.Helpers;
using GaminRoom.Domain.Models;
using Microsoft.AspNetCore.Mvc;

namespace GamingRoom.Controllers
{
    [Route("api/[controller]")]
    public class AccountController : BaseController
    {
        public AccountController(GamingRoomContext db) : base(db)
        {
        }

        // POST api/values
        [HttpPost]
        public ActionResult<User> Login([FromBody] LoginDto login)
        {
            var user = Users.FirstOrDefault(x => x.Username == login.Username && HashPassword.GeneratePassword(x.Password) == HashPassword.GeneratePassword(login.Password));
            if (user == null)
                throw new Exception("Invalid username/password");

            return user;
        }
    }
}
