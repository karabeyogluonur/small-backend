using System;
using AutoMapper;
using MediatR;
using SM.Core.Common;
using SM.Core.Common.Helpers;
using SM.Core.Domain;
using SM.Core.Interfaces.Collections;
using SM.Core.Interfaces.Services.Auth;
using SM.Core.Interfaces.Services.Blog;

namespace SM.Core.Features.Articles.GetAllArticle
{
    public class GetAllArticleHandler : IRequestHandler<GetAllArticleRequest, ApiResponse<GetAllArticleResponse>>
    {
        private readonly IArticleService _articleService;
        private readonly ITopicService _topicService;
        private readonly IMapper _mapper;
        public GetAllArticleHandler(IArticleService articleService, IMapper mapper, ITopicService topicService)
        {
            _articleService = articleService;
            _mapper = mapper;
            _topicService = topicService;
        }
        public async Task<ApiResponse<GetAllArticleResponse>> Handle(GetAllArticleRequest request, CancellationToken cancellationToken)
        {

            IPagedList<Article> articles = await _articleService.GetAllArticlesAsync(
                pageIndex: request.PageIndex,
                pageSize: request.PageSize,
                topicIds: ParserHelper.ParseStringIdToIntArray(request.TopicIds)
                );

            GetAllArticleResponse getAllArticleResponse = _mapper.Map<GetAllArticleResponse>(articles);
            return ApiResponse<GetAllArticleResponse>.Successful(getAllArticleResponse, "Article receive successful.");

        }
    }
}

