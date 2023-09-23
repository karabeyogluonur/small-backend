using System;
using AutoMapper;
using MediatR;
using SM.Core.Common;
using SM.Core.Domain;
using SM.Core.Features.Users.GetFollower;
using SM.Core.Interfaces.Collections;
using SM.Core.Interfaces.Services.Blog;

namespace SM.Core.Features.Users.LikeHistory
{
    public class LikeHistoryHandler : IRequestHandler<LikeHistoryRequest, ApiResponse<LikeHistoryResponse>>
    {
        private readonly IArticleService _articleService;
        private readonly IMapper _mapper;

        public LikeHistoryHandler(IArticleService articleService, IMapper mapper)
        {
            _articleService = articleService;
            _mapper = mapper;
        }

        public async Task<ApiResponse<LikeHistoryResponse>> Handle(LikeHistoryRequest request, CancellationToken cancellationToken)
        {
            IPagedList<ArticleLike> articleLikes = await _articleService.GetArticleLikesByUserIdAsync(userId:request.UserId,pageIndex:request.PageIndex,pageSize:request.PageSize);

            LikeHistoryResponse likeHistoryResponse = _mapper.Map<LikeHistoryResponse>(articleLikes);

            return ApiResponse<LikeHistoryResponse>.Successful(likeHistoryResponse, "Like histories receive successful.");
        }
    }
}

