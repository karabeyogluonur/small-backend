using AutoMapper;
using MediatR;
using SM.Core.Common;
using SM.Core.Domain;
using SM.Core.DTOs.Blog;
using SM.Core.Interfaces.Collections;
using SM.Core.Interfaces.Services;
using SM.Core.Interfaces.Services.Blog;
using SM.Core.Interfaces.Services.Membership;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SM.Core.Features.Topics.SearchTopic
{
    public class SearchTopicHandler : IRequestHandler<SearchTopicRequest, ApiResponse<SearchTopicResponse>>
    {
        private readonly ITopicService _topicService;
        private readonly IMapper _mapper;
        private readonly IWorkContext _workContext;
        private readonly IUserService _userService;

        public SearchTopicHandler(ITopicService topicService, IMapper mapper, IUserService userService, IWorkContext workContext)
        {
            _topicService = topicService;
            _mapper = mapper;
            _userService = userService;
            _workContext = workContext;
        }
        public async Task<ApiResponse<SearchTopicResponse>> Handle(SearchTopicRequest request, CancellationToken cancellationToken)
        {
            int applicationUserId = await _workContext.GetAuthenticatedUserIdAsync();

            if (applicationUserId > 0)
            {
                SearchKeyword searchKeyword = await _userService.GetSearchKeywordByKeywordAsync(applicationUserId, request.SearchKeywords);

                if (searchKeyword == null)
                    await _userService.InsertSearchKeywordAsync(new SearchKeyword { AuthorId = applicationUserId, Keyword = request.SearchKeywords });
                else
                    _userService.UpdateSearchKeyword(searchKeyword);
            }

            IPagedList<Topic> topics = await _topicService.SearchTopicsAsync(searchKeywords: request.SearchKeywords, showDeactived: request.ShowDeactived, pageIndex:request.pageIndex,pageSize:request.pageSize);
            SearchTopicResponse searchTopicResponse = _mapper.Map<SearchTopicResponse>(topics); 

            return ApiResponse<SearchTopicResponse>.Successful(searchTopicResponse, "Topic search receive successful.");

        }
    }
}
