﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Contracts.User
{
    public class CreatingUserDto
    {
        public Guid Id { get; init; }
        public string? Name { get; init; }
        public string? Email { get; set; }
    }
}
