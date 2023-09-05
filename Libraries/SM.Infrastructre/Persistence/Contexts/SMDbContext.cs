using System.Reflection.Emit;
using System.Reflection.Metadata;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using SM.Core.Domain;

namespace SM.Infrastructre.Persistence.Contexts
{
    public class SMDbContext : IdentityDbContext<ApplicationUser,ApplicationRole,int>
    {
        public SMDbContext(DbContextOptions options) : base(options)
        {
            
        }
        public DbSet<Article> Articles { get; set; }
        public DbSet<Topic> Topics { get; set; }
        public DbSet<Comment> Comment { get; set; }
        public DbSet<CommentReply> CommentReplies { get; set; }
    }
}
