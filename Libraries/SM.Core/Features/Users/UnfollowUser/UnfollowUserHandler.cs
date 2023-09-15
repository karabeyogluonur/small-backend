using System;
using MediatR;
using SM.Core.Common;
using SM.Core.Domain;
using SM.Core.Features.Users.FollowUser;
using SM.Core.Interfaces.Services;
using SM.Core.Interfaces.Services.Membership;

namespace SM.Core.Features.Users.UnfollowUser
{
    public class UnfollowUserHandler : IRequestHandler<UnfollowUserRequest, ApiResponse<UnfollowUserResponse>>
    {
        private readonly IWorkContext _workContext;
        private readonly IUserService _userService;

        public UnfollowUserHandler(IWorkContext workContext, IUserService userService)
        {
            _workContext = workContext;
            _userService = userService;
        }

        public async Task<ApiResponse<UnfollowUserResponse>> Handle(UnfollowUserRequest request, CancellationToken cancellationToken)
        {
            if (request.UserId != await _workContext.GetAuthenticatedUserIdAsync())
                throw new UnauthorizedAccessException("You are not authorized for this user.");

            if (request.UserId == request.RecipientId)
                return ApiResponse<UnfollowUserResponse>.Error(null, "You can not unfollow yourself.");

            if (await _userService.GetUserByIdAsync(request.RecipientId) == null)
                return ApiResponse<UnfollowUserResponse>.Error(null, "No user to unfollow found.");

            Follow follow = await _userService.GetFollowAsync(request.UserId, request.RecipientId);

            if (follow == null)
                return ApiResponse<UnfollowUserResponse>.Error(null, "You are not following this user.");

            _userService.DeleteFollow(follow);

            return ApiResponse<UnfollowUserResponse>.Successful(null, "The user has been successfully unfollowed.");
        }
    }
}

