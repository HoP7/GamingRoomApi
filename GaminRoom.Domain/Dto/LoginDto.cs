﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace GaminRoom.Domain.Dto
{
    public class LoginDto
    {
        public string Username { get; set; }

        public string Password { get; set; }
    }
}
