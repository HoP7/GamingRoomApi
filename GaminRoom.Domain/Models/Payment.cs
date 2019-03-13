using System;
using System.Collections.Generic;
using System.Text;

namespace GaminRoom.Domain.Models
{
    public class Payment
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public User User { get; set; }

        public DateTime Date { get; set; }
        public int Coins { get; set; }
    }
}
