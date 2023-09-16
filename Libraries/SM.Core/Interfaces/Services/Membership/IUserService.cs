using SM.Core.Domain;
using SM.Core.Interfaces.Collections;
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
        Task<Follow> GetFollowAsync(int followerId, int followeeId);
        Task<IPagedList<Follow>> GetFollowersAsync(int userId, int pageIndex = 0,
            int pageSize = int.MaxValue);
        Task<IPagedList<Follow>> GetFolloweesAsync(int userId, int pageIndex = 0,
            int pageSize = int.MaxValue);
        Task InsertFollowAsync(Follow follow);
        void DeleteFollow(Follow follow);

    }
}
