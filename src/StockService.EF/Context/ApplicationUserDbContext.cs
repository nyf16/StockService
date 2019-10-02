using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using StockService.Domain.Identity;
using System;
using System.Collections.Generic;
using System.Text;

namespace StockService.EF.Context
{
    public class ApplicationUserDbContext : 
        IdentityDbContext<ApplicationUser>
    {
        public ApplicationUserDbContext
            (DbContextOptions<ApplicationUserDbContext>options) : base(options)
        {

        }

        /*
         DBSet ' ler buraya
         */

    }
}
