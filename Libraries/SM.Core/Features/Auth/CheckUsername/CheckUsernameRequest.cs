using System;
using MediatR;
using SM.Core.Common;

namespace SM.Core.Features.Auth.CheckUsername
{
	public class CheckUsernameRequest : IRequest<ApiResponse<CheckUsernameResponse>>
	{
		public string Username { get; set; }
	}
}

