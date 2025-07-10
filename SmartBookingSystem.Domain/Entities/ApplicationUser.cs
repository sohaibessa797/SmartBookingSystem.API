﻿using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartBookingSystem.Domain.Entities
{
    public class ApplicationUser : IdentityUser<Guid>
    {
        public string FirstName { get; set; } = string.Empty;   
        public string LastName { get; set; } = string.Empty;

        public Customer? Customer { get; set; }
        public Provider? Provider { get; set; }
    }
}
