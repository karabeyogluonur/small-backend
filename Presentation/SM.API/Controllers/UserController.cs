using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SM.Core.Common;
using SM.Core.Features.Users.CheckFollow;
using SM.Core.Features.Users.FollowUser;
using SM.Core.Features.Users.GetArticle;
using SM.Core.Features.Users.GetByUsername;
using SM.Core.Features.Users.GetFollowed;
using SM.Core.Features.Users.GetFollower;
using SM.Core.Features.Users.UnfollowUser;

namespace SM.API.Controllers
{
    [Route("api/users")]
    public class UserController : Controller
    {
        private readonly IMediator _mediator;

        public UserController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("get-by-username")]
        public async Task<IActionResult> GetByUsername([FromQuery] GetByUsernameRequest getByUsernameRequest)
        {
            ApiResponse<GetByUsernameResponse> apiResponse = await _mediator.Send(getByUsernameRequest);
            return Ok(apiResponse);
        }

        [HttpPost("{userId}/follow/{recipientId}")]
        [Authorize]
        public async Task<IActionResult> FollowUser([FromRoute] FollowUserRequest followUserRequest)
        {
            ApiResponse<FollowUserResponse> apiResponse = await _mediator.Send(followUserRequest);
            if (apiResponse.Success)
                return Ok(apiResponse);
            else
                return BadRequest(apiResponse);
        }
        [HttpPost("{userId}/unfollow/{recipientId}")]
        [Authorize]
        public async Task<IActionResult> UnfollowUser([FromRoute] UnfollowUserRequest unfollowUserRequest)
        {
            ApiResponse<UnfollowUserResponse> apiResponse = await _mediator.Send(unfollowUserRequest);
            if (apiResponse.Success)
                return Ok(apiResponse);
            else
                return BadRequest(apiResponse);
        }
        [HttpGet("{userId}/followers")]
        public async Task<IActionResult> GetFollowers([FromRoute] GetFollowerRequest getFollowerRequest)
        {
            ApiResponse<GetFollowerResponse> apiResponse = await _mediator.Send(getFollowerRequest);
            if (apiResponse.Success)
                return Ok(apiResponse);
            else
                return BadRequest(apiResponse);
        }
        [HttpGet("{userId}/followed")]
        public async Task<IActionResult> GetFollowed([FromRoute] GetFollowedRequest getFollowedRequest)
        {
            ApiResponse<GetFollowedResponse> apiResponse = await _mediator.Send(getFollowedRequest);
            if (apiResponse.Success)
                return Ok(apiResponse);
            else
                return BadRequest(apiResponse);
        }
        [HttpGet("{userId}/check-follow/{recipientId}")]
        public async Task<IActionResult> CheckFollow([FromRoute] CheckFollowRequest checkFollowRequest)
        {
            ApiResponse<CheckFollowResponse> apiResponse = await _mediator.Send(checkFollowRequest);
            if (apiResponse.Success)
                return Ok(apiResponse);
            else
                return BadRequest(apiResponse);
        }
        [HttpGet("{userId}/articles")]
        public async Task<IActionResult> GetArticles(GetArticleRequest getArticleRequest)
        {
            ApiResponse<GetArticleResponse> apiResponse = await _mediator.Send(getArticleRequest);
            if (apiResponse.Success)
                return Ok(apiResponse);
            else
                return BadRequest(apiResponse);
        }
    }
}

