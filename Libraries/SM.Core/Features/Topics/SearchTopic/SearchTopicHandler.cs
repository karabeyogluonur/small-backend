using AutoMapper;
using MediatR;
using SM.Core.Common;
using SM.Core.Domain;
using SM.Core.DTOs.Blog;
using SM.Core.Interfaces.Services.Blog;
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

        public SearchTopicHandler(ITopicService topicService, IMapper mapper)
        {
            _topicService = topicService;
            _mapper = mapper;
        }
        public async Task<ApiResponse<SearchTopicResponse>> Handle(SearchTopicRequest request, CancellationToken cancellationToken)
        {
            List<Topic> topics = await _topicService.SearchTopicsAsync(searchKeywords: request.SearchKeywords, showDeactived: request.ShowDeactived);

            SearchTopicResponse searchTopicResponse = new SearchTopicResponse
            {
                Topics = _mapper.Map<List<TopicDTO>>(topics)
            };

            return ApiResponse<SearchTopicResponse>.Successful(searchTopicResponse, "Topic search receive successful.");

        }
    }
}
