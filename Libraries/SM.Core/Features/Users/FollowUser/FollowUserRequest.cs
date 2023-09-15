using System;
using System.Xml.Linq;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using SM.Core.Common;

namespace SM.Core.Features.Users.FollowUser
{
	public class FollowUserRequest : IRequest<ApiResponse<FollowUserResponse>>
	{
        [FromRoute(Name = "userId")]
        public int UserId { get; set; }
        [FromRoute(Name = "recipientId")]
        public int RecipientId { get; set; }
	}
}

