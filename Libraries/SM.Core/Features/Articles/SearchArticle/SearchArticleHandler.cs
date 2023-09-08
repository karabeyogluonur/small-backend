using System;
using AutoMapper;
using MediatR;
using SM.Core.Common;
using SM.Core.Domain;
using SM.Core.Features.Topics.SearchTopic;
using SM.Core.Interfaces.Collections;
using SM.Core.Interfaces.Services.Blog;

namespace SM.Core.Features.Articles.SearchArticle
{
    public class SearchArticleHandler : IRequestHandler<SearchArticleRequest, ApiResponse<SearchArticleResponse>>
    {
        private readonly IArticleService _articleService;
        private readonly IMapper _mapper;

        public SearchArticleHandler(IArticleService articleService, IMapper mapper)
        {
            _articleService = articleService;
            _mapper = mapper;
        }

        public async Task<ApiResponse<SearchArticleResponse>> Handle(SearchArticleRequest request, CancellationToken cancellationToken)
        {
            IPagedList<Article> articles = await _articleService.SearchArticlesAsync(searchKeywords: request.SearchKeywords, pageIndex: request.pageIndex, pageSize: request.pageSize);
            SearchArticleResponse searchArticleResponse = _mapper.Map<SearchArticleResponse>(articles);

            return ApiResponse<SearchArticleResponse>.Successful(searchArticleResponse, "Article search receive successful.");
        }
    }
}

