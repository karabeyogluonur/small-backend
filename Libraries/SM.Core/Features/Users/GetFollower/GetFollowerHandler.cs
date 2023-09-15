using System;
using AutoMapper;
using MediatR;
using SM.Core.Common;
using SM.Core.Common.Helpers;
using SM.Core.Domain;
using SM.Core.Features.Articles.GetAllArticle;
using SM.Core.Interfaces.Collections;
using SM.Core.Interfaces.Services;
using SM.Core.Interfaces.Services.Blog;
using SM.Core.Interfaces.Services.Membership;

namespace SM.Core.Features.Users.GetFollower
{
    public class GetFollowerHandler : IRequestHandler<GetFollowerRequest, ApiResponse<GetFollowerResponse>>
    {
        private readonly IWorkContext _workContext;
        private readonly IUserService _userService;
        private readonly IMapper _mapper;

        public GetFollowerHandler(IWorkContext workContext, IUserService userService, IMapper mapper)
        {
            _workContext = workContext;
            _userService = userService;
            _mapper = mapper;
        }
        public async Task<ApiResponse<GetFollowerResponse>> Handle(GetFollowerRequest request, CancellationToken cancellationToken)
        {
            IPagedList<Follow> followers = await _userService.GetFollowersAsync(
                pageIndex: request.PageIndex,
                pageSize: request.PageSize,
                userId: request.UserId
                );

            GetFollowerResponse getFollowerResponse = _mapper.Map<GetFollowerResponse>(followers);
            return ApiResponse<GetFollowerResponse>.Successful(getFollowerResponse, "Follewers receive successful.");


        }
    }
}

