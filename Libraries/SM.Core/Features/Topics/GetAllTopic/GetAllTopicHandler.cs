using AutoMapper;
using MediatR;
using SM.Core.Common;
using SM.Core.Domain;
using SM.Core.DTOs.Blog;
using SM.Core.Interfaces.Collections;
using SM.Core.Interfaces.Services.Blog;

namespace SM.Core.Features.Topics.GetAllTopic
{
    public class GetAllTopicHandler : IRequestHandler<GetAllTopicRequest, ApiResponse<GetAllTopicResponse>>
    {
        private readonly ITopicService _topicService;
        private readonly IMapper _mapper;

        public GetAllTopicHandler(ITopicService topicService, IMapper mapper)
        {
            _topicService = topicService;
            _mapper = mapper;
        }

        public async Task<ApiResponse<GetAllTopicResponse>> Handle(GetAllTopicRequest request, CancellationToken cancellationToken)
        {
            IPagedList<Topic> topics = await _topicService.GetAllTopicsAsync(showDeactived: request.ShowDeactived,pageIndex:request.pageIndex,pageSize:request.pageSize);
            GetAllTopicResponse getAllTopicsResponse = _mapper.Map<GetAllTopicResponse>(topics);

            return ApiResponse<GetAllTopicResponse>.Successful(getAllTopicsResponse, "Topics receive successful.");
        }
    }
}
