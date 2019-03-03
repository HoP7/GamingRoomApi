using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace GaminRoom.Domain.Models
{
    public class Transaction
    {
        public int Id { get; set; }

        public DateTime TransactionDate { get; set; }

        public float Hours { get; set; }

        public string Code { get; set; }

        public int Coins { get; set; }

        public User User { get; set; }
        [ForeignKey("User")]
        public int UserId { get; set; }

        
    }
}
