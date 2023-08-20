using Microsoft.AspNetCore.Identity;
using SM.Core.Domain;
using SM.Core.DTOs.Auth;
using SM.Core.Features.Auth.Register;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SM.Core.Interfaces.Services.Auth
{
    public interface IAuthService
    {
        Task<TokenDTO> SignInAsync(string email, string password);
        Task<IdentityResult> RegisterAsync(ApplicationUser applicationUser,string password);
        Task<TokenDTO> RefreshTokenSignInAsync(string refreshToken);
        Task<string> ForgotPasswordAsync(string email);
        Task<bool> ResetPasswordConfirmationAsync(string resetPasswordToken);
        Task<IdentityResult> ResetPasswordAsync(string resetPasswordToken,string newPassword);
    }
}
