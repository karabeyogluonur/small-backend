using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using SM.Core.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SM.Infrastructre.Persistence.Contexts
{
    public class SMDbContext : IdentityDbContext<ApplicationUser,ApplicationRole,int>
    {
        public SMDbContext(DbContextOptions options) : base(options)
        {
            
        }
        public DbSet<Topic> Topics { get; set; }
    }
}
