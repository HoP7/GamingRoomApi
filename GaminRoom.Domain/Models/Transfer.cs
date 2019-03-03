using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace GaminRoom.Domain.Models
{
    public class Transfer
    {
        public int Id { get; set; }

        public int Coins { get; set; }

        public DateTime TransferDate { get; set; }
        public User Receiver { get; set; }

        public User Sender { get; set; }

        [ForeignKey("Receiver")]
        public int ReceiverId { get; set; }
        [ForeignKey("Sender")]
        public int SenderId { get; set; }
    }
}
