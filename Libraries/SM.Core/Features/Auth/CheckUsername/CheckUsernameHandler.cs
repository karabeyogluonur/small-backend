using System;
using MediatR;
using SM.Core.Common;
using SM.Core.Domain;
using SM.Core.Interfaces.Services.Membership;

namespace SM.Core.Features.Auth.CheckUsername
{
    public class CheckUsernameHandler : IRequestHandler<CheckUsernameRequest, ApiResponse<CheckUsernameResponse>>
    {
        private readonly IUserService _userService;

        public CheckUsernameHandler(IUserService userService)
        {
            _userService = userService;
        }

        public async Task<ApiResponse<CheckUsernameResponse>> Handle(CheckUsernameRequest request, CancellationToken cancellationToken)
        {
            ApplicationUser applicationUser = await _userService.GetUserByUserNameAsync(request.Username);

            if (applicationUser == null)
                return ApiResponse<CheckUsernameResponse>.Successful(new CheckUsernameResponse { isAvailable = true },"Username is available.");
            else
                return ApiResponse<CheckUsernameResponse>.Successful(new CheckUsernameResponse { isAvailable = false }, "Username is unavailable.");
        }
    }
}

