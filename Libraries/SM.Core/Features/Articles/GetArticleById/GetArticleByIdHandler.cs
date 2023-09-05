using System;
using AutoMapper;
using MediatR;
using SM.Core.Common;
using SM.Core.Domain;
using SM.Core.DTOs.Blog;
using SM.Core.Interfaces.Services.Auth;
using SM.Core.Interfaces.Services.Blog;

namespace SM.Core.Features.Articles.GetArticleById
{
    public class GetArticleByIdHandler : IRequestHandler<GetArticleByIdRequest, ApiResponse<GetArticleByIdResponse>>
    {
        private readonly IArticleService _articleService;
        private readonly IMapper _mapper;
        private readonly IAuthService _authService;

        public GetArticleByIdHandler(IArticleService articleService, IMapper mapper, IAuthService authService)
        {
            _articleService = articleService;
            _mapper = mapper;
            _authService = authService;
        }

        public async Task<ApiResponse<GetArticleByIdResponse>> Handle(GetArticleByIdRequest request, CancellationToken cancellationToken)
        {
            Article article = await _articleService.GetArticleByIdAsync(request.articleId);

            if (article == null)
                return ApiResponse<GetArticleByIdResponse>.Error(null, "Article not found");

            GetArticleByIdResponse getArticleByIdResponse = new GetArticleByIdResponse { Article = _mapper.Map<ArticleDTO>(article) };
            return ApiResponse<GetArticleByIdResponse>.Successful(getArticleByIdResponse, "The article has been successfully listed.");
            
        }
    }
}

