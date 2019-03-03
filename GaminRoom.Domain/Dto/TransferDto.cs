using System;
using System.Collections.Generic;
using System.Text;

namespace GaminRoom.Domain.Dto
{
    public class TransferDto
    {
        public int UserId { get; set; }
        public string UserCode { get; set; }
        public int Coins { get; set; }
    }
}
