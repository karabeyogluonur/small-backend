using MediatR;
using SM.Core.Common;
using SM.Core.Interfaces.Services.Auth;
using SM.Core.Interfaces.Services.Notification;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SM.Core.Features.Auth.ForgotPassword
{
    public class ForgotPasswordHandle : IRequestHandler<ForgotPasswordRequest, ApiResponse<ForgotPasswordResponse>>
    {
        private readonly IAuthService _authService;
        private readonly IEmailService _emailService;

        public ForgotPasswordHandle(IAuthService authService, IEmailService emailService)
        {
            _authService = authService;
            _emailService = emailService;
        }

        public async Task<ApiResponse<ForgotPasswordResponse>> Handle(ForgotPasswordRequest request, CancellationToken cancellationToken)
        {
            string refreshPasswordToken = await _authService.ForgotPasswordAsync(request.Email);
            await _emailService.SendForgotPasswordEmailAsync(refreshPasswordToken,request.Email);

            return ApiResponse<ForgotPasswordResponse>.Successful(null, "Password reset email sent successfully.");
           
        }
    }
}
