using System;
using AutoMapper;
using MediatR;
using SM.Core.Common;
using SM.Core.Domain;
using SM.Core.DTOs.Membership;
using SM.Core.Interfaces.Services;

namespace SM.Core.Features.Profiles.Me
{
    public class MeHandler : IRequestHandler<MeRequest, ApiResponse<MeResponse>>
    {
        private readonly IWorkContext _workContext;
        private readonly IMapper _mapper;

        public MeHandler(IWorkContext workContext, IMapper mapper)
        {
            _workContext = workContext;
            _mapper = mapper;
        }

        public async Task<ApiResponse<MeResponse>> Handle(MeRequest request, CancellationToken cancellationToken)
        {
            ApplicationUser author = await _workContext.GetAuthenticatedUserAsync();

            MeResponse meResponse = new MeResponse { User = _mapper.Map<ApplicationUserDTO>(author) };

            return ApiResponse<MeResponse>.Successful(meResponse, "Profile information has been fetched successfully.");

        }
    }
}

