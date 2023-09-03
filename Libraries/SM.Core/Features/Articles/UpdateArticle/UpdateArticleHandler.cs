using System;
using AutoMapper;
using MediatR;
using SM.Core.Common;
using SM.Core.Domain;
using SM.Core.DTOs.Blog;
using SM.Core.Interfaces.Services.Auth;
using SM.Core.Interfaces.Services.Blog;

namespace SM.Core.Features.Articles.UpdateArticle
{
    public class UpdateArticleHandler : IRequestHandler<UpdateArticleRequest, ApiResponse<UpdateArticleResponse>>
    {
        private readonly IArticleService _articleService;
        private readonly IAuthService _authService;
        private readonly IMapper _mapper;

        public UpdateArticleHandler(IArticleService articleService, IAuthService authService, IMapper mapper)
        {
            _articleService = articleService;
            _authService = authService;
            _mapper = mapper;
        }

        public async Task<ApiResponse<UpdateArticleResponse>> Handle(UpdateArticleRequest request, CancellationToken cancellationToken)
        {
            Article article = await _articleService.GetArticleByIdAsync(request.ArticleId);

            if (article == null)
                return ApiResponse<UpdateArticleResponse>.Error(null, "Article not found.");

            ApplicationUser currentUser = await _authService.GetAuthenticatedCustomerAsync();

            if (article.AuthorId != currentUser.Id)
                throw new UnauthorizedAccessException("You are not authorized for this article");

            article = _mapper.Map(request, article);

            _articleService.UpdateArticle(article);

            UpdateArticleResponse updateArticleRequest = _mapper.Map<UpdateArticleResponse>(article);

            return ApiResponse<UpdateArticleResponse>.Successful(updateArticleRequest, "The article has been successfully updated");


        }
    }
}

