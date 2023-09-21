using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SM.Core.Domain
{
    public class ApplicationUser : IdentityUser<int>
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string? RefreshToken { get; set; }
        public DateTime? RefreshTokenExpiration { get; set; }
        public string? PasswordResetToken { get; set; }
        public string? AvatarImagePath { get; set; }

        public ICollection<Follow> Follower { get; set; }
        public ICollection<Follow> Followee { get; set; }

        public ICollection<ArticleLike> ArticleLikes { get; set; }
    }
}
 