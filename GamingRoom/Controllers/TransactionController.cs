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
    [AuthorizationAttribute]
    [Route("api/[controller]")]
    public class TransactionController : BaseController
    {
        public TransactionController(GamingRoomContext db) : base(db)
        {
        }

        // GET api/values
        [HttpGet]
        public ActionResult<IEnumerable<Transaction>> Get()
        {
            return Transactions;
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public ActionResult<Transaction> Get(int id)
        {
            return Transactions.FirstOrDefault(x => x.Id == id);
        }
        [HttpGet("payments")]
        public ActionResult<List<Payment>> Payments()
        {
           var user = HttpContext.GetUser();
var payments =    _db.Payments.Where(x => x.UserId == user.Id).ToList();
            return payments;
        }
        [HttpPost("addcoins")]
        public void AddCoins([FromBody] AddCoinsDto addCoinsDto)
        {
            var user = Users.FirstOrDefault(x => x.Id == addCoinsDto.UserId);
            user.Coins += addCoinsDto.Coins;
            user.AddedFromLastBonus += addCoinsDto.Coins;

            if(user.AddedFromLastBonus >= 500)
            {
                user.AddedFromLastBonus -= 500;
                user.Coins += 100;
            }
            _db.Users.Update(user);
            _db.Payments.Add(new Payment { Date = DateTime.UtcNow, Coins = addCoinsDto.Coins, UserId = user.Id });
            _db.SaveChanges();
        }

        // POST api/values
        [HttpPost]
        public IActionResult Post([FromBody] TransactionDto transaction)
        {
            var user = Users.FirstOrDefault(x => x.Id == transaction.UserId);
            if (user == null)
                return StatusCode(500,"User doesnt exist");
            var coins = transaction.Hours * 50;
            if (coins > user.Coins)
                return StatusCode(500,"You don't have enough coins to play with");

            user.Coins -= (int)(coins);
            var randomCode = RandomString.GenerateString(7);
            var t = new Transaction
            {
                Coins = (int)coins,
                Hours = transaction.Hours,
                Code = randomCode,
                TransactionDate = DateTime.UtcNow,
                UserId = transaction.UserId

            };
            _db.Users.Update(user);
            _db.Transactions.Add(t);
            _db.SaveChanges();
            return Ok(randomCode);
        }

        // PUT api/values/5
        [HttpPut]
        public void Put([FromBody] Transaction transaction)
        {
            _db.Transactions.Update(transaction);
            _db.SaveChanges();
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            _db.Transactions.Remove(Transactions.FirstOrDefault(x => x.Id == id));
        }
    }
}
