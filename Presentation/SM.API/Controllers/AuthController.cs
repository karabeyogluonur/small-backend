using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SM.Core.Common;
using SM.Core.Features.Auth.Login;
using SM.Core.Features.Auth.Register;

namespace SM.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IMediator _mediator;

        public AuthController(IMediator mediator)
        {
            _mediator = mediator;
        }


        [HttpPost]
        [Route("login")]
        public async Task<IActionResult> Login(LoginRequest loginRequest)
        {
            ApiResponse<LoginResponse> apiResponse = await _mediator.Send(loginRequest);

            AddRefreshTokenToCookie(apiResponse.Data.Expiration, apiResponse.Data.RefreshToken);

            return Ok(apiResponse);
        }

        [HttpPost]
        [Route("register")]
        public async Task<IActionResult> Register(RegisterRequest registerRequest)
        {
            ApiResponse<RegisterResponse> apiResponse = await _mediator.Send(registerRequest);
            if (apiResponse.Success)
            {
                AddRefreshTokenToCookie(apiResponse.Data.Expiration, apiResponse.Data.RefreshToken);
                return Ok(apiResponse);
            }
            else
                return BadRequest(apiResponse);
        }

        [HttpGet]
        [Route("verify")]
        [Authorize]
        public async Task<IActionResult> Verify()
        {
            return Ok(ApiResponse<object>.Successful(null, "Your identity is successful."));
        }

        private void AddRefreshTokenToCookie(DateTime expiration, string refreshToken)
        {
            CookieOptions cookieOptions = new CookieOptions();
            cookieOptions.Expires = expiration;
            cookieOptions.HttpOnly = true;
            cookieOptions.Secure = true;
            cookieOptions.SameSite = SameSiteMode.None;

            Response.Cookies.Append("refreshToken", refreshToken, cookieOptions);
        }
    }
}
