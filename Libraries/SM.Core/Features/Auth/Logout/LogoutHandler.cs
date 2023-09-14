using System;
using MediatR;
using SM.Core.Common;
using SM.Core.Domain;
using SM.Core.Interfaces.Services;
using SM.Core.Interfaces.Services.Auth;
using SM.Core.Interfaces.Services.Membership;

namespace SM.Core.Features.Auth.Logout
{
	public class LogoutHandler : IRequestHandler<LogoutRequest,ApiResponse<LogoutResponse>>
	{
        private readonly IAuthService _authService;
        private readonly IWorkContext _workContext;
        private readonly IUserService _userService;

        public LogoutHandler(IAuthService authService, IWorkContext workContext, IUserService userService)
        {
            _authService = authService;
            _workContext = workContext;
            _userService = userService;
        }

        public async Task<ApiResponse<LogoutResponse>> Handle(LogoutRequest request, CancellationToken cancellationToken)
        {
            ApplicationUser applicationUser = await _workContext.GetAuthenticatedUserAsync();
            await _userService.UpdateRefreshTokenAsync(applicationUser, "");

            await _authService.SignOutAsync();     
            return ApiResponse<LogoutResponse>.Successful(null, "The session has been closed successfully.");
        }
    }
}

