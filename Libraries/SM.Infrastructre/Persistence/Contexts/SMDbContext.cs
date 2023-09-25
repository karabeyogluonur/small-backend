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
        public DbSet<Follow> Follows { get; set; }
        public DbSet<ArticleLike> ArticleLikes { get; set; }
        public DbSet<SearchKeyword> SearchKeywords { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {

            #region Follow
            builder.Entity<Follow>()
                .HasKey(k => new { k.FollowerId, k.FolloweeId });

            builder.Entity<Follow>()
                .HasOne(u => u.Followee)
                .WithMany(u => u.Followee)
                .HasForeignKey(u => u.FolloweeId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.Entity<Follow>()
                .HasOne(u => u.Follower)
                .WithMany(u => u.Follower)
                .HasForeignKey(u => u.FollowerId)
                .OnDelete(DeleteBehavior.Restrict);
            #endregion

            #region Article Like

            builder.Entity<ArticleLike>()
                .HasKey(k => new { k.AuthorId, k.ArticleId });

            builder.Entity<ArticleLike>()
                .HasOne(u => u.Article)
                .WithMany(u => u.Likes)
                .HasForeignKey(u => u.ArticleId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.Entity<ArticleLike>()
                .HasOne(u => u.Author)
                .WithMany(u => u.ArticleLikes)
                .HasForeignKey(u => u.AuthorId)
                .OnDelete(DeleteBehavior.Restrict);

            #endregion

            base.OnModelCreating(builder);

        }
    }
}
