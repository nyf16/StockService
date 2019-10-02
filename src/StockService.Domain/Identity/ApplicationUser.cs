using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;

namespace StockService.Domain.Identity
{
    public class ApplicationUser : IdentityUser
    {
        public int RegisterNumber { get; set; }
    }
}
