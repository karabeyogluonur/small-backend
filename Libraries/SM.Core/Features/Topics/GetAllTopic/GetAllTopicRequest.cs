using MediatR;
using SM.Core.Common;
using SM.Core.Common.Enums.Collections;

namespace SM.Core.Features.Topics.GetAllTopic
{
    public class GetAllTopicRequest : IRequest<ApiResponse<GetAllTopicResponse>>
    {
        public bool ShowDeactived { get; set; }
        public int pageIndex { get; set; }
        public int pageSize { get; set; } = (int)DefaultPageSize.GetAllTopic;
    }
}
