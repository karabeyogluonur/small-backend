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
        Task<ApplicationUser> GetUserByUserNameAsync(string userName);
        Task<ApplicationUser> GetUserByIdAsync(int userId);
        Task<Follow> GetFollowAsync(int followeeId, int followerId);
        Task InsertFollowAsync(Follow follow);
        void DeleteFollow(Follow follow);

    }
}
