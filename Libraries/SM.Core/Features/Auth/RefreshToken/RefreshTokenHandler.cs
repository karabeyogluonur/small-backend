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

namespace SM.Core.Features.Auth.RefreshToken
{
    public class RefreshTokenHandler : IRequestHandler<RefreshTokenRequest, ApiResponse<RefreshTokenResponse>>
    {
        private readonly IAuthService _authService;
        private readonly IMapper _mapper;

        public RefreshTokenHandler(IAuthService authService, IMapper mapper)
        {
            _authService = authService;
            _mapper = mapper;
        }
        public async Task<ApiResponse<RefreshTokenResponse>> Handle(RefreshTokenRequest request, CancellationToken cancellationToken)
        {
            TokenDTO tokenDTO = await _authService.RefreshTokenSignInAsync(request.RefreshToken);

            RefreshTokenResponse refreshTokenResponse = _mapper.Map<RefreshTokenResponse>(tokenDTO);
            return ApiResponse<RefreshTokenResponse>.Successful(refreshTokenResponse, "Access token refreshed.");
        }
    }
}
