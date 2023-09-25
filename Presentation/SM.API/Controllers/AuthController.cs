using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SM.Core.Common;
using SM.Core.Features.Auth.CheckUsername;
using SM.Core.Features.Auth.ForgotPassword;
using SM.Core.Features.Auth.Login;
using SM.Core.Features.Auth.Logout;
using SM.Core.Features.Auth.RefreshPasswordConfirmation;
using SM.Core.Features.Auth.RefreshToken;
using SM.Core.Features.Auth.Register;
using SM.Core.Features.Auth.ResetPassword;

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
        [HttpDelete]
        [Route("logout")]
        [Authorize]
        public async Task<IActionResult> Logout()
        {
            ApiResponse<LogoutResponse> apiResponse = await _mediator.Send(new LogoutRequest());
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

        [HttpPost("refresh-token")]
        public async Task<IActionResult> RefreshToken(RefreshTokenRequest? refreshTokenRequest)
        {
            #region Read cookie for refresh token

            if (refreshTokenRequest == null || String.IsNullOrEmpty(refreshTokenRequest.RefreshToken))
            {
                string refreshToken = HttpContext.Request.Cookies.FirstOrDefault(cookie => cookie.Key == "refreshToken").Value;

                if (String.IsNullOrEmpty(refreshToken))
                    return Unauthorized(ApiResponse<object>.Error(null, "You are not authenticated."));

                else
                    refreshTokenRequest = new RefreshTokenRequest(){RefreshToken = refreshToken};
            }

            #endregion

            ApiResponse<RefreshTokenResponse> apiResponse = await _mediator.Send(refreshTokenRequest);
            AddRefreshTokenToCookie(apiResponse.Data.Expiration, apiResponse.Data.RefreshToken);
            return Ok(apiResponse);
        }

        [HttpPost("forgot-password")]
        public async Task<IActionResult> ForgotPassword(ForgotPasswordRequest forgotPasswordRequest)
        {
            ApiResponse<ForgotPasswordResponse> apiResponse = await _mediator.Send(forgotPasswordRequest);
            return Ok(apiResponse);
        }

        [HttpPost("reset-password-confirmation")]
        public async Task<IActionResult> ResetPasswordConfirmation(ResetPasswordConfirmationRequest resetPasswordConfirmationRequest)
        {
            ApiResponse<ResetPasswordConfirmationResponse> apiResponse = await _mediator.Send(resetPasswordConfirmationRequest);

            if (apiResponse.Success)
                return Ok(apiResponse);
            else
                return BadRequest(apiResponse);
        }

        [HttpPost("reset-password")]
        public async Task<IActionResult> ResetPassword(ResetPasswordRequest resetPasswordRequest)
        {
            ApiResponse<ResetPasswordResponse> apiResponse = await _mediator.Send(resetPasswordRequest);

            if (apiResponse.Success)
                return Ok(apiResponse);
            else
                return BadRequest(apiResponse);
        }

        [HttpPost("check-username")]
        public async Task<IActionResult> CheckUsername(CheckUsernameRequest checkUsernameRequest)
        {
            ApiResponse<CheckUsernameResponse> apiResponse = await _mediator.Send(checkUsernameRequest);

            if (apiResponse.Success)
                return Ok(apiResponse);
            else
                return BadRequest(apiResponse);
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
