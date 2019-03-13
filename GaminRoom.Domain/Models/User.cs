using System;
using System.Collections.Generic;
using System.Text;

namespace GaminRoom.Domain.Models
{
    public class User
    {
        public int Id { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Email { get; set; }

        public string Username { get; set; }

        public string Password { get; set; }

        public string FullName => FirstName + " " + LastName;
        public int AddedFromLastBonus { get; set; }

        public int Coins { get; set; }

        public byte[] Salt { get; set; }
        public List<Transfer> Incoming { get; set; }

        public List<Transfer> Outgoing { get; set; }

        public string Token { get; set; }
    }
}
