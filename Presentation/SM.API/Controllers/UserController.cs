using MediatR;
using Microsoft.AspNetCore.Mvc;
using SM.Core.Common;
using SM.Core.Features.Users.FollowUser;
using SM.Core.Features.Users.GetByUsername;

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
        public async Task<IActionResult> FollowUser([FromRoute] FollowUserRequest followUserRequest)
        {
            ApiResponse<FollowUserResponse> apiResponse = await _mediator.Send(followUserRequest);
            if (apiResponse.Success)
                return Ok(apiResponse);
            else
                return BadRequest(apiResponse);
        }

    }
}

