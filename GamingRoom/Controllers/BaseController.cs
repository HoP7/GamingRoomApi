using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GaminRoom.Domain;
using GaminRoom.Domain.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace GamingRoom.Controllers
{
    public class BaseController : ControllerBase
    {
        public GamingRoomContext _db;
        protected List<User> Users { get; set; }
        protected List<Transfer> Transfers { get; set; }
        protected List<Transaction> Transactions { get; set; }
        protected List<Code> Codes { get; set; }

        public BaseController(GamingRoomContext db)
        {
            _db = db;
            Users = _db.Users.ToList();
            Transfers = _db.Transfers.ToList();
            Transactions = _db.Transactions.ToList();
            Codes = _db.Codes.ToList();
        }
    }
}