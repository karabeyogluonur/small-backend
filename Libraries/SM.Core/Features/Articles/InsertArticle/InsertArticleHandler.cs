using System;
using MediatR;
using SM.Core.Common;
using SM.Core.Domain;
using SM.Core.Interfaces.Services.Auth;
using SM.Core.Interfaces.Services.Blog;

namespace SM.Core.Features.Articles.InsertArticle
{
    public class InsertArticleHandler : IRequestHandler<InsertArticleRequest, ApiResponse<InsertArticleResponse>>
    {
        private readonly IArticleService _articleService;
        private readonly IAuthService _authService;
        public InsertArticleHandler(IArticleService articleService, IAuthService authService)
        {
            _articleService = articleService;
            _authService = authService;
        }
        public async Task<ApiResponse<InsertArticleResponse>> Handle(InsertArticleRequest request, CancellationToken cancellationToken)
        {
            ApplicationUser applicationUser = await _authService.GetAuthenticatedCustomerAsync();
            if (applicationUser == null)
                throw new UnauthorizedAccessException("User not found");

            int articleId = await _articleService.InsertArticleAsync(new Article()
            {
                Title = request.Title,
                Content = request.Content,
                ApplicationUserId = applicationUser.Id
            });

            InsertArticleResponse insertArticleResponse = new InsertArticleResponse { Id = articleId };
            return ApiResponse<InsertArticleResponse>.Successful(insertArticleResponse, "The article has been successfully added.");
            
        }
    }
}

