﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Domain.Models
{
    public class Users
    {
        public int UsersId { get; set; }
        public string? UserName { get; set; }
        public string? Email { get; set; }
        public int Otp { get; set; }
        public string? Password { get; set; }
    }
}
