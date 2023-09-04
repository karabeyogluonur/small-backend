using System;
using MediatR;
using SM.Core.Common;
using SM.Core.Domain;
using SM.Core.Interfaces.Services.Auth;
using SM.Core.Interfaces.Services.Blog;

namespace SM.Core.Features.Articles.PublishArticle
{
    public class PublishArticleHandler : IRequestHandler<PublishArticleRequest, ApiResponse<PublishArticleResponse>>
    {
        private readonly IArticleService _articleService;
        private readonly IAuthService _authService;

        public PublishArticleHandler(IArticleService articleService, IAuthService authService)
        {
            _articleService = articleService;
            _authService = authService;
        }

        public async Task<ApiResponse<PublishArticleResponse>> Handle(PublishArticleRequest request, CancellationToken cancellationToken)
        {
            Article article = await _articleService.GetArticleByIdAsync(request.ArticleId);

            if (article == null || article.Deleted)
                return ApiResponse<PublishArticleResponse>.Error(null, "Article is not found");

            ApplicationUser author = await _authService.GetAuthenticatedCustomerAsync();

            if (article.AuthorId == author.Id)
                throw new UnauthorizedAccessException("You are not authorized for this article.");

            if (article.Published)
                return ApiResponse<PublishArticleResponse>.Error(null, "The article has already been published.");

            if (string.IsNullOrEmpty(article.Title) || string.IsNullOrEmpty(article.Content))
                return ApiResponse<PublishArticleResponse>.Error(null, "Title and content of the article cannot be empty");

            article.Published = true;
            _articleService.UpdateArticle(article);

            return ApiResponse<PublishArticleResponse>.Successful(null, "The article has been published.");
        }
    }
}

