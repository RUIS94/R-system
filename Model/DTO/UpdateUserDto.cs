﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.DTO
{
    public class UpdateUserDto
    {
        public string? UserName { get; set;  }
        public string? Password { get; set; }
        public string? Phone { get; set; }
        public string? Email { get; set; }
        public int RoleID { get; set; }
    }
}
