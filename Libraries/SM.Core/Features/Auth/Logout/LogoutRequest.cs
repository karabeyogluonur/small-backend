using System;
using MediatR;
using SM.Core.Common;

namespace SM.Core.Features.Auth.Logout
{
    public class LogoutRequest : IRequest<ApiResponse<LogoutResponse>>
	{
		
	}
}

