using MediatR;
using SM.Core.Common;
using SM.Core.Interfaces.Services.Auth;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SM.Core.Features.Auth.RefreshPasswordConfirmation
{
    public class ResetPasswordConfirmationHandle : IRequestHandler<ResetPasswordConfirmationRequest, ApiResponse<ResetPasswordConfirmationResponse>>
    {
        private readonly IAuthService _authService;

        public ResetPasswordConfirmationHandle(IAuthService authService)
        {
            _authService = authService;
        }

        public async Task<ApiResponse<ResetPasswordConfirmationResponse>> Handle(ResetPasswordConfirmationRequest request, CancellationToken cancellationToken)
        {
            bool resetPasswordTokenCheck = await _authService.ResetPasswordConfirmationAsync(request.ResetPasswordToken);

            if (resetPasswordTokenCheck)
                return ApiResponse<ResetPasswordConfirmationResponse>.Successful(null, "The password reset token is valid.");
            else
                return ApiResponse<ResetPasswordConfirmationResponse>.Error(null, "The password reset token is incorrect.");

        }
    }
}
