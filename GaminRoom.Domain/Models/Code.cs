using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace GaminRoom.Domain.Models
{
    public class Code
    {
        public int Id { get; set; }

        public User User { get; set; }
        [ForeignKey("User")]
        public int UserId { get; set; }

        public DateTime DateExpired { get; set; }

        public string GeneratedCode { get; set; }
    }
}
