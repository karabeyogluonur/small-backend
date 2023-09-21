using System;
using MediatR;
using SM.Core.Common;
using SM.Core.Domain;
using SM.Core.Interfaces.Services;
using SM.Core.Interfaces.Services.Blog;

namespace SM.Core.Features.Articles.LikeArticle
{
    public class LikeArticleHandler : IRequestHandler<LikeArticleRequest, ApiResponse<LikeArticleResponse>>
	{
        private readonly IArticleService _articleService;
        private readonly IWorkContext _workContext;

        public LikeArticleHandler(IArticleService articleService, IWorkContext workContext)
        {
            _articleService = articleService;
            _workContext = workContext;
        }

        public async Task<ApiResponse<LikeArticleResponse>> Handle(LikeArticleRequest request, CancellationToken cancellationToken)
        {          
            Article article = await _articleService.GetArticleByIdAsync(request.ArticleId);

            if(article == null || article.Published == false || article.Deleted == true)
                return ApiResponse<LikeArticleResponse>.Error(null, "Article not found!");

            int authorId = await _workContext.GetAuthenticatedUserIdAsync();

            ArticleLike articleLike = await _articleService.GetArticleLikeAsync(authorId: authorId, articleId: request.ArticleId);

            if (articleLike != null)
                return ApiResponse<LikeArticleResponse>.Error(null, "This article has been liked before.");

            await _articleService.InsertArticleLikeAsync(new ArticleLike() { ArticleId = request.ArticleId, AuthorId = authorId });

            return ApiResponse<LikeArticleResponse>.Successful(null, "The article liking process has been completed successfully.");


        }
    }
}

