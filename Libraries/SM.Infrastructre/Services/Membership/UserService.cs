using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using SM.Core.Domain;
using SM.Core.Interfaces.Services.Membership;


namespace SM.Infrastructre.Services.Membership
{
    public class UserService : IUserService
    {
        private readonly IConfiguration _configuration;
        private readonly UserManager<ApplicationUser> _userManager;

        public UserService(UserManager<ApplicationUser> userManager, IConfiguration configuration)
        {
            _userManager = userManager;
            _configuration = configuration;
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
