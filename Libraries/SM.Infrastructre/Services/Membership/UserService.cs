using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using SM.Core.Common.Enums.Media;
using SM.Core.Domain;
using SM.Core.Interfaces.Services.Media;
using SM.Core.Interfaces.Services.Membership;


namespace SM.Infrastructre.Services.Membership
{
    public class UserService : IUserService
    {
        private readonly IConfiguration _configuration;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IFileService _fileService;

        public UserService(UserManager<ApplicationUser> userManager, IConfiguration configuration,IFileService fileService)
        {
            _userManager = userManager;
            _configuration = configuration;
            _fileService = fileService;
        }

        public async Task ChangeAvatarImageAsync(string avatarImageName, int userId)
        {
            ApplicationUser applicationUser = await _userManager.FindByIdAsync(userId.ToString());

            if (applicationUser.AvatarImagePath != null)
                await _fileService.DeleteAsync(applicationUser.AvatarImagePath, RegisteredFileType.Avatars);

            applicationUser.AvatarImagePath = avatarImageName;
            await _userManager.UpdateAsync(applicationUser);
        }

        public async Task<ApplicationUser> GetUserByUserName(string userName)
        {
            return await _userManager.FindByNameAsync(userName);
        }

        public async Task UpdatePasswordResetTokenAsync(ApplicationUser applicationUser, string passwordResetToken)
        {
            applicationUser.PasswordResetToken = passwordResetToken;

            await _userManager.UpdateAsync(applicationUser);
        }

        public async Task UpdateRefreshTokenAsync(ApplicationUser applicationUser, string refreshToken)
        {
            applicationUser.RefreshToken = refreshToken;
            applicationUser.RefreshTokenExpiration = DateTime.UtcNow.AddMinutes(Convert.ToInt32(_configuration["JWT:RefreshTokenExpirationTime"]));

            await _userManager.UpdateAsync(applicationUser);
        }
    }
}
