using System;
using AutoMapper;
using MediatR;
using SM.Core.Common;
using SM.Core.Domain;
using SM.Core.Features.Users.GetFollower;
using SM.Core.Interfaces.Collections;
using SM.Core.Interfaces.Services;
using SM.Core.Interfaces.Services.Membership;

namespace SM.Core.Features.Users.GetFollowed
{
    public class GetFollowedHandler : IRequestHandler<GetFollowedRequest, ApiResponse<GetFollowedResponse>>
	{
        private readonly IUserService _userService;
        private readonly IMapper _mapper;

        public GetFollowedHandler(IUserService userService, IMapper mapper)
        {
            _userService = userService;
            _mapper = mapper;
        }
        public async Task<ApiResponse<GetFollowedResponse>> Handle(GetFollowedRequest request, CancellationToken cancellationToken)
        {
            IPagedList<Follow> followees = await _userService.GetFolloweesAsync(
                pageIndex: request.PageIndex,
                pageSize: request.PageSize,
                userId: request.UserId
                );

            GetFollowedResponse getFollowedResponse = _mapper.Map<GetFollowedResponse>(followees);
            return ApiResponse<GetFollowedResponse>.Successful(getFollowedResponse, "Followeds receive successful.");
        }
    }
}

