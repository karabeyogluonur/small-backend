using System;
using AutoMapper;
using MediatR;
using SM.Core.Common;
using SM.Core.Domain;
using SM.Core.DTOs.Blog;
using SM.Core.Interfaces.Services;
using SM.Core.Interfaces.Services.Membership;

namespace SM.Core.Features.Users.RecentSearch
{
    public class RecentSearchHandler : IRequestHandler<RecentSearchRequest, ApiResponse<RecentSearchResponse>>
    {
        private readonly IUserService _userService;
        private readonly IWorkContext _workContext;
        private readonly IMapper _mapper;

        public RecentSearchHandler(IUserService userService, IWorkContext workContext, IMapper mapper)
        {
            _userService = userService;
            _workContext = workContext;
            _mapper = mapper;
        }

        public async Task<ApiResponse<RecentSearchResponse>> Handle(RecentSearchRequest request, CancellationToken cancellationToken)
        {
            if(request.UserId != await _workContext.GetAuthenticatedUserIdAsync())
                throw new UnauthorizedAccessException("You are not authorized for this user.");

            List<SearchKeyword> searchKeywords = await _userService.GetSearchKeywordsByUserIdAsync(request.UserId);

            return ApiResponse<RecentSearchResponse>.Successful(new RecentSearchResponse { SearchKeywords = _mapper.Map<List<SearchKeywordDTO>>(searchKeywords) }, "Recent searches receive successful.");

        }
    }
}

