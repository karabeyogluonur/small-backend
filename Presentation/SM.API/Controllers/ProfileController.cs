using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SM.Core.Common;
using SM.Core.Features.Profiles.Me;
using SM.Core.Features.Profiles.UploadProfileImage;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace SM.API.Controllers
{
    [Route("api/profiles")]
    [Authorize]
    public class ProfileController : Controller
    {
        private readonly IMediator _mediator;

        public ProfileController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("me")]      
        public async Task<IActionResult> Me()
        {
            ApiResponse<MeResponse> apiResponse = await _mediator.Send(new MeRequest());

            if (apiResponse.Success)
                return Ok(apiResponse);
            else
                return BadRequest(apiResponse);
        }
        [HttpPost("upload-avatar-image")]
        public async Task<IActionResult> UploadAvatarImage([FromForm]UploadAvatarImageRequest UploadAvatarImageRequest)
        {
            ApiResponse<UploadAvatarImageResponse> apiResponse = await _mediator.Send(UploadAvatarImageRequest);

            if (apiResponse.Success)
                return Ok(apiResponse);
            else
                return BadRequest(apiResponse);
        }
    }
}

