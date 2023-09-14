using SM.Core.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SM.Core.Interfaces.Services.Membership
{
    public interface IUserService
    {
        Task UpdateRefreshTokenAsync(ApplicationUser applicationUser, string refreshToken);
        Task UpdatePasswordResetTokenAsync(ApplicationUser applicationUser, string passwordResetToken);
        Task ChangeAvatarImageAsync(string avatarImageName, int userId);
        Task<ApplicationUser> GetUserByUserName(string userName);
    }
}
