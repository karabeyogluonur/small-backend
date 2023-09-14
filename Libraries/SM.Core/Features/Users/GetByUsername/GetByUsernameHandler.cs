using System;
using AutoMapper;
using MediatR;
using SM.Core.Common;
using SM.Core.Domain;
using SM.Core.DTOs.Membership;
using SM.Core.Interfaces.Services.Membership;

namespace SM.Core.Features.Users.GetByUsername
{
	public class GetByUsernameHandler : IRequestHandler<GetByUsernameRequest,ApiResponse<GetByUsernameResponse>>
	{
        private readonly IUserService _userService;
        private readonly IMapper _mapper;

        public GetByUsernameHandler(IUserService userService, IMapper mapper)
        {
            _userService = userService;
            _mapper = mapper;
        }

        public async Task<ApiResponse<GetByUsernameResponse>> Handle(GetByUsernameRequest request, CancellationToken cancellationToken)
        {
            ApplicationUser applicationUser =  await _userService.GetUserByUserName(request.Username);
            GetByUsernameResponse getByUsernameRequest = new GetByUsernameResponse { User = _mapper.Map<ApplicationUserDTO>(applicationUser) };

            return ApiResponse<GetByUsernameResponse>.Successful(getByUsernameRequest, "User fetching completed successfully.");
            
        }
    }
}

