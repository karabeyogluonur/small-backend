using System;
using MediatR;
using SM.Core.Common;
using SM.Core.Domain;
using SM.Core.Interfaces.Services.Blog;

namespace SM.Core.Features.Articles.PublishArticle
{
    public class PublishArticleHandler : IRequestHandler<PublishArticleRequest, ApiResponse<PublishArticleResponse>>
    {
        private readonly IArticleService _articleService;

        public PublishArticleHandler(IArticleService articleService)
        {
            _articleService = articleService;
        }

        public async Task<ApiResponse<PublishArticleResponse>> Handle(PublishArticleRequest request, CancellationToken cancellationToken)
        {
            Article article = await _articleService.GetArticleByIdAsync(request.ArticleId);

            if (article == null || article.Deleted)
                return ApiResponse<PublishArticleResponse>.Error(null, "Article is not found");

            if(article.Published)
                return ApiResponse<PublishArticleResponse>.Error(null, "The article has already been published.");

            if (string.IsNullOrEmpty(article.Title) || string.IsNullOrEmpty(article.Content))
                return ApiResponse<PublishArticleResponse>.Error(null, "Title and content of the article cannot be empty");

            article.Published = true;
            _articleService.UpdateArticle(article);

            return ApiResponse<PublishArticleResponse>.Successful(null, "The article has been published.");
        }
    }
}

