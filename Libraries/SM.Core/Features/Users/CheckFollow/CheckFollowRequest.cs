using System;
using Microsoft.AspNetCore.Mvc;
using System.Xml.Linq;
using SM.Core.Common;
using MediatR;

namespace SM.Core.Features.Users.CheckFollow
{
    public class CheckFollowRequest : IRequest<ApiResponse<CheckFollowResponse>>
	{
        [FromRoute(Name = "userId")]
        public int UserId { get; set; }
        [FromRoute(Name = "recipientId")]
        public int RecipientId { get; set; }
    }
}

