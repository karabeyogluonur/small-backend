using System;
using MediatR;
using SM.Core.Common;
using SM.Core.Domain;
using SM.Core.Interfaces.Services;
using SM.Core.Interfaces.Services.Blog;

namespace SM.Core.Features.Articles.CheckArticleLike
{
    public class CheckArticleLikeHandler : IRequestHandler<CheckArticleLikeRequest, ApiResponse<CheckArticleLikeResponse>>
    {
        private readonly IArticleService _articleService;
        private readonly IWorkContext _workContext;

        public CheckArticleLikeHandler(IArticleService articleService, IWorkContext workContext)
        {
            _articleService = articleService;
            _workContext = workContext;
        }

        public async Task<ApiResponse<CheckArticleLikeResponse>> Handle(CheckArticleLikeRequest request, CancellationToken cancellationToken)
        {
            int authorId = await _workContext.GetAuthenticatedUserIdAsync();

            ArticleLike articleLike = await _articleService.GetArticleLikeAsync(authorId: authorId, articleId: request.ArticleId);

            if (articleLike == null)
                return ApiResponse<CheckArticleLikeResponse>.Successful(new CheckArticleLikeResponse() { IsLike = false }, "Article liking process has been checked successfully.");
            else
                return ApiResponse<CheckArticleLikeResponse>.Successful(new CheckArticleLikeResponse() { IsLike = true }, "Article liking process has been checked successfully.");
        }
    }
}

