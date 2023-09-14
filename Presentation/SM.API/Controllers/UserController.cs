using MediatR;
using Microsoft.AspNetCore.Mvc;
using SM.Core.Common;
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
    }
}

