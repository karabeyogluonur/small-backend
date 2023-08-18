using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Identity;
using SM.Core.Common;
using SM.Core.Domain;
using SM.Core.DTOs.Auth;
using SM.Core.Interfaces.Services.Auth;

namespace SM.Core.Features.Auth.Register
{
    public class RegisterHandler : IRequestHandler<RegisterRequest, ApiResponse<RegisterResponse>>
    {
        private readonly IAuthService _authService;
        private readonly IMapper _mapper;

        public RegisterHandler(IAuthService authService, IMapper mapper)
        {
            _authService = authService;
            _mapper = mapper;
        }

        public async Task<ApiResponse<RegisterResponse>> Handle(RegisterRequest request, CancellationToken cancellationToken)
        {
            ApplicationUser applicationUser =  _mapper.Map<ApplicationUser>(request);
            IdentityResult identityResult = await _authService.RegisterAsync(applicationUser,request.Password);

            if (identityResult.Succeeded)
            {
                TokenDTO tokenDTO = await _authService.SignInAsync(applicationUser.Email, request.Password);
                RegisterResponse registerResponse = _mapper.Map<RegisterResponse>(tokenDTO);
                return ApiResponse<RegisterResponse>.Successful(registerResponse, "Registration completed successfully.");
            }
            else
            {
                var errors = identityResult.Errors.ToDictionary(error => error.Code, error => error.Description).ToList();
                return ApiResponse<RegisterResponse>.Error(errors, "Received user data is not available.");
            }
        }
    }
}
