﻿using System;
using System.Xml.Linq;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using SM.Core.Common;
using SM.Core.Common.Enums.Collections;

namespace SM.Core.Features.Users.GetFollower
{
	public class GetFollowerRequest : IRequest<ApiResponse<GetFollowerResponse>>
	{
        [FromRoute(Name = "userId")]
        public int UserId { get; set; }
        [FromQuery]
        public int PageIndex { get; set; }
        [FromQuery]
        public int PageSize { get; set; } = (int)DefaultPageSize.GetFollower;
    }
}

