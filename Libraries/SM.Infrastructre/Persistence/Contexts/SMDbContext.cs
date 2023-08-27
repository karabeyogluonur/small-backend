using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using SM.Core.Domain;

namespace SM.Infrastructre.Persistence.Contexts
{
    public class SMDbContext : IdentityDbContext<ApplicationUser,ApplicationRole,int>
    {
        public SMDbContext(DbContextOptions options) : base(options)
        {
            
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseLazyLoadingProxies();
        }
        public DbSet<Topic> Topics { get; set; }
    }
}
