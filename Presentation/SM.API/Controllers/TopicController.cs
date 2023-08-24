using MediatR;
using Microsoft.AspNetCore.Mvc;
using SM.Core.Common;
using SM.Core.Domain;
using SM.Core.Features.Topics.GetAllTopic;
using SM.Core.Features.Topics.SearchTopic;
using SM.Core.Interfaces.Services.Blog;

namespace SM.API.Controllers
{
    [Route("api/topics")]
    [ApiController]
    public class TopicController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly ITopicService _topicService;

        public TopicController(IMediator mediator, ITopicService topicService)
        {
            _mediator = mediator;
            _topicService = topicService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] GetAllTopicRequest getAllTopicsRequest)
        {
            ApiResponse<GetAllTopicResponse> apiResponse = await _mediator.Send(getAllTopicsRequest);
            return Ok(apiResponse);
        }

        [HttpGet("search")]
        public async Task<IActionResult> Search([FromQuery] SearchTopicRequest searchTopicRequest)
        {
            ApiResponse<SearchTopicResponse> apiResponse = await _mediator.Send(searchTopicRequest);
            return Ok(apiResponse);
        }
    }
}
