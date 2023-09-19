using System;
using MediatR;
using SM.Core.Common;
using SM.Core.Domain;
using SM.Core.Features.Users.UnfollowUser;
using SM.Core.Interfaces.Services;
using SM.Core.Interfaces.Services.Membership;

namespace SM.Core.Features.Users.CheckFollow
{
    public class CheckFollowHandler : IRequestHandler<CheckFollowRequest, ApiResponse<CheckFollowResponse>>
    {
        private readonly IWorkContext _workContext;
        private readonly IUserService _userService;

        public CheckFollowHandler(IWorkContext workContext, IUserService userService)
        {
            _workContext = workContext;
            _userService = userService;
        }

        public async Task<ApiResponse<CheckFollowResponse>> Handle(CheckFollowRequest request, CancellationToken cancellationToken)
        {
            if (request.UserId == request.RecipientId)
                return ApiResponse<CheckFollowResponse>.Error(null, "You can not follow yourself.");

            if (await _userService.GetUserByIdAsync(request.RecipientId) == null)
                return ApiResponse<CheckFollowResponse>.Error(null, "User not found. Id : " + request.RecipientId);

            Follow follow = await _userService.GetFollowAsync(followeeId: request.RecipientId, followerId: request.UserId);

            if (follow != null)
                return ApiResponse<CheckFollowResponse>.Error(new CheckFollowResponse { isFollow = true }, "You are following this user.");
            else
                return ApiResponse<CheckFollowResponse>.Error(null, "You are not following this user.");
        }
    }
}

