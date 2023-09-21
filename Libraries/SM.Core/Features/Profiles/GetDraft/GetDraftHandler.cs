using System;
using AutoMapper;
using MediatR;
using SM.Core.Common;
using SM.Core.Domain;
using SM.Core.Features.Users.GetArticle;
using SM.Core.Interfaces.Collections;
using SM.Core.Interfaces.Services;
using SM.Core.Interfaces.Services.Blog;

namespace SM.Core.Features.Profiles.GetDraft
{
    public class GetDraftHandler : IRequestHandler<GetDraftRequest, ApiResponse<GetDraftResponse>>
    {
        private readonly IArticleService _articleService;
        private readonly IWorkContext _workContext;
        private readonly IMapper _mapper;

        public GetDraftHandler(IArticleService articleService, IWorkContext workContext, IMapper mapper)
        {
            _articleService = articleService;
            _workContext = workContext;
            _mapper = mapper;
        }

        public async Task<ApiResponse<GetDraftResponse>> Handle(GetDraftRequest request, CancellationToken cancellationToken)
        {
            int applicationUserId = await _workContext.GetAuthenticatedUserIdAsync();

            IPagedList<Article> drafts = await _articleService.GetDraftsByUserIdAsync(userId: applicationUserId, pageIndex: request.PageIndex, pageSize: request.PageSize);

            GetDraftResponse getDraftResponse = _mapper.Map<GetDraftResponse>(drafts);
            return ApiResponse<GetDraftResponse>.Successful(getDraftResponse, "Drafts receive successful.");
        }
    }
}

