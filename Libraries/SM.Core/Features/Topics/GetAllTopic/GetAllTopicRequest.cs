using MediatR;
using SM.Core.Common;

namespace SM.Core.Features.Topics.GetAllTopic
{
    public class GetAllTopicRequest : IRequest<ApiResponse<GetAllTopicResponse>>
    {
        public bool ShowDeactived { get; set; }
    }
}
