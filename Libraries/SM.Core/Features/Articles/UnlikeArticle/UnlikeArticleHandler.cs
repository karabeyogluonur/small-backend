using System;
using MediatR;
using SM.Core.Common;
using SM.Core.Domain;
using SM.Core.Features.Articles.LikeArticle;
using SM.Core.Interfaces.Services;
using SM.Core.Interfaces.Services.Blog;

namespace SM.Core.Features.Articles.UnlikeArticle
{
    public class UnlikeArticleHandler : IRequestHandler<UnlikeArticleRequest, ApiResponse<UnlikeArticleResponse>>
    {
        private readonly IArticleService _articleService;
        private readonly IWorkContext _workContext;

        public UnlikeArticleHandler(IArticleService articleService, IWorkContext workContext)
        {
            _articleService = articleService;
            _workContext = workContext;
        }

        public async Task<ApiResponse<UnlikeArticleResponse>> Handle(UnlikeArticleRequest request, CancellationToken cancellationToken)
        {
            int authorId = await _workContext.GetAuthenticatedUserIdAsync();

            ArticleLike articleLike = await _articleService.GetArticleLikeAsync(authorId: authorId, articleId: request.ArticleId);

            if (articleLike == null)
                return ApiResponse<UnlikeArticleResponse>.Error(null, "This article is not liked.");

            _articleService.DeleteArticleLike(articleLike);

            return ApiResponse<UnlikeArticleResponse>.Successful(null, "The like was successfully deleted.");

        }
    }
}

