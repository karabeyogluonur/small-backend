using System;
using AutoMapper;
using MediatR;
using SM.Core.Common;
using SM.Core.Common.Helpers;
using SM.Core.Domain;
using SM.Core.Features.Articles.GetAllArticle;
using SM.Core.Interfaces.Collections;
using SM.Core.Interfaces.Services;
using SM.Core.Interfaces.Services.Blog;

namespace SM.Core.Features.Users.GetArticle
{
    public class GetArticleHandler : IRequestHandler<GetArticleRequest, ApiResponse<GetArticleResponse>>
    {
        private readonly IArticleService _articleService;
        private readonly IWorkContext _workContext;
        private readonly IMapper _mapper;

        public GetArticleHandler(IArticleService articleService, IMapper mapper, IWorkContext workContext)
        {
            _articleService = articleService;
            _mapper = mapper;
            _workContext = workContext;
        }

        public async Task<ApiResponse<GetArticleResponse>> Handle(GetArticleRequest request, CancellationToken cancellationToken)
        {
            IPagedList<Article> articles = await _articleService.GetAllArticlesAsync(
                userId: request.UserId,
                pageIndex: request.PageIndex,
                pageSize: request.PageSize,
                showNonPublished:false
                );

            GetArticleResponse getAllArticleResponse = _mapper.Map<GetArticleResponse>(articles);
            return ApiResponse<GetArticleResponse>.Successful(getAllArticleResponse, "Article receive successful.");
        }
    }
}

