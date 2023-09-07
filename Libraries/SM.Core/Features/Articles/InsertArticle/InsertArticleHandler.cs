using System;
using MediatR;
using SM.Core.Common;
using SM.Core.Domain;
using SM.Core.Interfaces.Services;
using SM.Core.Interfaces.Services.Auth;
using SM.Core.Interfaces.Services.Blog;

namespace SM.Core.Features.Articles.InsertArticle
{
    public class InsertArticleHandler : IRequestHandler<InsertArticleRequest, ApiResponse<InsertArticleResponse>>
    {
        private readonly IArticleService _articleService;
        private readonly IAuthService _authService;
        private readonly IWorkContext _workContext;

        public InsertArticleHandler(IArticleService articleService, IAuthService authService, IWorkContext workContext)
        {
            _articleService = articleService;
            _authService = authService;
            _workContext = workContext;
        }
        public async Task<ApiResponse<InsertArticleResponse>> Handle(InsertArticleRequest request, CancellationToken cancellationToken)
        {

            int articleId = await _articleService.InsertArticleAsync(new Article()
            {
                Title = request.Title,
                Content = request.Content,
                AuthorId = await _workContext.GetAuthenticatedUserIdAsync()
            });

            InsertArticleResponse insertArticleResponse = new InsertArticleResponse { Id = articleId };
            return ApiResponse<InsertArticleResponse>.Successful(insertArticleResponse, "The article has been successfully added.");
            
        }
    }
}

