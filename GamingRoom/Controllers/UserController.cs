using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GaminRoom.Domain;
using GaminRoom.Domain.Helpers;
using GaminRoom.Domain.Models;
using Microsoft.AspNetCore.Mvc;

namespace GamingRoom.Controllers
{
    [Route("api/[controller]")]
    public class UserController : BaseController
    {
        public UserController(GamingRoomContext db) : base(db)
        {
        }

        // GET api/values
        [HttpGet]
        public ActionResult<IEnumerable<User>> Get()
        {
            return Ok(Users);
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public ActionResult<User> Get(int id)
        {
            var user = Users.FirstOrDefault(x => x.Id == id);
            return user;
        }

        // POST api/values
        [HttpPost]
        public void Post([FromBody] User user)
        {
            if (string.IsNullOrEmpty(user.Password) || string.IsNullOrEmpty(user.Username))
                throw new Exception("User cannot have empty password or username");

            if(user.Password.Length < 6)
            {
                throw new Exception("User password need to have min 6 signs");
            }
            var password = HashPassword.GeneratePassword(user.Password);
            user.Password = password;

            var existingUser = Users.FirstOrDefault(x => x.Username == user.Username || x.Email == user.Email);
            if (existingUser != null)
                throw new Exception("Already exist user with that username or email");
            _db.Users.Add(user);
            _db.SaveChanges();
        }

        // PUT api/values/5
        [HttpPut]
        public void Put([FromBody] User user)
        {
            if (string.IsNullOrEmpty(user.Password) || string.IsNullOrEmpty(user.Username))
                throw new Exception("User cannot have empty password or username");

            if (user.Password.Length < 6)
            {
                throw new Exception("User password need to have min 6 signs");
            }
            var newPassword = HashPassword.GeneratePassword(user.Password);
            user.Password = newPassword;
            var existingUser = Users.FirstOrDefault(x => (x.Username == user.Username || x.Email == user.Email) && x.Id != user.Id);
            if (existingUser != null)
                throw new Exception("Already exist user with that username or email");
            _db.Users.Update(user);
            _db.SaveChanges();
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            var user = Users.FirstOrDefault(x => x.Id == id);
            _db.Users.Remove(user);
            _db.SaveChanges();
        }

        [HttpGet("topplayers")]
        public ActionResult TopPlayers()
        {
            var transactions = Transactions.GroupBy(x => x.UserId)
                                   .Select(g => new { User = Users.FirstOrDefault(y => y.Id == g.Key) ,
                                   Coins = g.Sum(c => c.Coins)}).ToList();

            return Ok(transactions);
        }
    }
}
