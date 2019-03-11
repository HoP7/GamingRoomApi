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
    public class TransferController : BaseController
    {
        public TransferController(GamingRoomContext db) : base(db)
        {
        }

        // GET api/values
        [HttpGet]
        public ActionResult<IEnumerable<Transfer>> Get()
        {
            return Transfers;
        }

        [HttpGet("in")]
        public ActionResult<IEnumerable<Transfer>> GetIncoming(int userId)
        {
            return Transfers.Where(x => x.ReceiverId == userId).ToList();
        }

        [HttpGet("out")]
        public ActionResult<IEnumerable<Transfer>> GetOutcoming(int userId)
        {
            return Transfers.Where(x => x.SenderId == userId).ToList();
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public ActionResult<Transfer> Get(int id)
        {
            return Transfers.FirstOrDefault(x => x.Id == id);
        }

        // POST api/values
        [HttpPost]
        public void Post([FromBody] TransferDto transfer)
        {
            if (string.IsNullOrEmpty(transfer.UserCode))
                throw new Exception("User code is required");

            var code = Codes.FirstOrDefault(x => x.GeneratedCode == transfer.UserCode && new DateTime() < x.DateExpired);
            if (code == null)
                throw new Exception("Code doesn't exist or its expired");

            var t = new Transfer
            {
                Coins = transfer.Coins,
                ReceiverId = code.UserId,
                SenderId = transfer.UserId,
                TransferDate = new DateTime()
            };
            var receiver = Users.FirstOrDefault(x => x.Id == code.UserId);
            var sender = Users.FirstOrDefault(x => x.Id == transfer.UserId);
            receiver.Coins += transfer.Coins;
            sender.Coins -= transfer.Coins;
            _db.Users.Update(receiver);
            _db.Users.Update(sender);
            _db.Transfers.Add(t);
            _db.SaveChanges();
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put([FromBody] Transfer transfer)
        {
            _db.Transfers.Update(transfer);
            _db.SaveChanges();
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            _db.Transfers.Remove(Transfers.FirstOrDefault(x => x.Id == id));    
        }
    }
}
