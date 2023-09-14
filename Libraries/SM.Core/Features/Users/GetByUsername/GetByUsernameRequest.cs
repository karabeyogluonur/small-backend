using System;
using MediatR;
using SM.Core.Common;

namespace SM.Core.Features.Users.GetByUsername
{
	public class GetByUsernameRequest : IRequest<ApiResponse<GetByUsernameResponse>>
	{
		public string Username { get; set; }
	}
}

