using System;
using MediatR;
using SM.Core.Common;
using SM.Core.Domain;
using SM.Core.Interfaces.Services;
using SM.Core.Interfaces.Services.Membership;

namespace SM.Core.Features.Users.FollowUser
{
    public class FollowUserHandler : IRequestHandler<FollowUserRequest, ApiResponse<FollowUserResponse>>
    {
        private readonly IWorkContext _workContext;
        private readonly IUserService _userService;

        public FollowUserHandler(IWorkContext workContext, IUserService userService)
        {
            _workContext = workContext;
            _userService = userService;
        }

        public async Task<ApiResponse<FollowUserResponse>> Handle(FollowUserRequest request, CancellationToken cancellationToken)
        {
            if (request.UserId != await _workContext.GetAuthenticatedUserIdAsync())
                throw new UnauthorizedAccessException("You are not authorized for this user.");

            if(request.UserId == request.RecipientId)
                return ApiResponse<FollowUserResponse>.Error(null, "You can not follow yourself.");

            if (await _userService.GetUserByIdAsync(request.RecipientId) == null)
                return ApiResponse<FollowUserResponse>.Error(null, "No user to follow found");

            Follow follow = await _userService.GetFollowAsync(request.UserId, request.RecipientId);

            if (follow != null)
                return ApiResponse<FollowUserResponse>.Error(null, "You already followed this user.");

            await _userService.InsertFollowAsync(new Follow { FollowerId = request.UserId, FolloweeId = request.RecipientId });

            return ApiResponse<FollowUserResponse>.Successful(null, "The user has been successfully followed.");

        }
    }
}

