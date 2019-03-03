using System;
using System.Collections.Generic;
using System.Text;

namespace GaminRoom.Domain.Dto
{
    public class AddCoinsDto
    {
        public int UserId { get; set; }

        public int Coins { get; set; }
    }
}
