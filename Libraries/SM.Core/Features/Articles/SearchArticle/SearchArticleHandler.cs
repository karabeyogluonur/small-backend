using System;
using AutoMapper;
using MediatR;
using SM.Core.Common;
using SM.Core.Domain;
using SM.Core.Features.Topics.SearchTopic;
using SM.Core.Interfaces.Collections;
using SM.Core.Interfaces.Services;
using SM.Core.Interfaces.Services.Blog;
using SM.Core.Interfaces.Services.Membership;

namespace SM.Core.Features.Articles.SearchArticle
{
    public class SearchArticleHandler : IRequestHandler<SearchArticleRequest, ApiResponse<SearchArticleResponse>>
    {
        private readonly IArticleService _articleService;
        private readonly IUserService _userService;
        private readonly IWorkContext _workContext;
        private readonly IMapper _mapper;

        public SearchArticleHandler(IArticleService articleService, IMapper mapper, IUserService userService, IWorkContext workContext)
        {
            _articleService = articleService;
            _mapper = mapper;
            _userService = userService;
            _workContext = workContext;
        }

        public async Task<ApiResponse<SearchArticleResponse>> Handle(SearchArticleRequest request, CancellationToken cancellationToken)
        {
            int applicationUserId =  await _workContext.GetAuthenticatedUserIdAsync();

            if (applicationUserId > 0)
            {
               SearchKeyword searchKeyword = await _userService.GetSearchKeywordByKeywordAsync(applicationUserId, request.SearchKeywords);

                if(searchKeyword == null)
                    await _userService.InsertSearchKeywordAsync(new SearchKeyword { AuthorId = applicationUserId, Keyword = request.SearchKeywords });
                else
                    _userService.UpdateSearchKeyword(searchKeyword);
            }
                

            IPagedList<Article> articles = await _articleService.SearchArticlesAsync(searchKeywords: request.SearchKeywords, pageIndex: request.pageIndex, pageSize: request.pageSize);
            SearchArticleResponse searchArticleResponse = _mapper.Map<SearchArticleResponse>(articles);

            return ApiResponse<SearchArticleResponse>.Successful(searchArticleResponse, "Article search receive successful.");
        }
    }
}

