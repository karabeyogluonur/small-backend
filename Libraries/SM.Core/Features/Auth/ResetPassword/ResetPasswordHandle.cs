using MediatR;
using Microsoft.AspNetCore.Identity;
using SM.Core.Common;
using SM.Core.Domain;
using SM.Core.DTOs.Auth;
using SM.Core.Features.Auth.Register;
using SM.Core.Interfaces.Services.Auth;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SM.Core.Features.Auth.ResetPassword
{
    public class ResetPasswordHandle : IRequestHandler<ResetPasswordRequest, ApiResponse<ResetPasswordResponse>>
    {
        private readonly IAuthService _authService;
        public ResetPasswordHandle(IAuthService authService)
        {
            _authService = authService;
        }
        public async Task<ApiResponse<ResetPasswordResponse>> Handle(ResetPasswordRequest request, CancellationToken cancellationToken)
        {
            IdentityResult identityResult = await _authService.ResetPasswordAsync(request.ResetPasswordToken, request.Password);
            if (identityResult.Succeeded)
                return ApiResponse<ResetPasswordResponse>.Successful(null, "Password change successful.");

            else
            {
                var errors = identityResult.Errors.ToDictionary(error => error.Code, error => error.Description).ToList();
                return ApiResponse<ResetPasswordResponse>.Error(errors, "Password change failed.");
            }
        }
    }
}
