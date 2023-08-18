using AutoMapper;
using MediatR;
using SM.Core.Common;
using SM.Core.DTOs.Auth;
using SM.Core.Interfaces.Services.Auth;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SM.Core.Features.Auth.Login
{
    public class LoginHandler : IRequestHandler<LoginRequest, ApiResponse<LoginResponse>>
    {

        private readonly IAuthService _authService;
        private readonly IMapper _mapper;
        public LoginHandler(IAuthService authService, IMapper mapper)
        {
            _authService = authService;
            _mapper = mapper;
        }

        public async Task<ApiResponse<LoginResponse>> Handle(LoginRequest request, CancellationToken cancellationToken)
        {
            TokenDTO tokenDTO = await _authService.SignInAsync(request.Email,request.Password);

            LoginResponse loginResponse = _mapper.Map<LoginResponse>(tokenDTO);

            return ApiResponse<LoginResponse>.Successful(loginResponse, "Login was successful.");
        }
    }
}
