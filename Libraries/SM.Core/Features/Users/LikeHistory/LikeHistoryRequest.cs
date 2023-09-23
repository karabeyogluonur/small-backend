using System;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using SM.Core.Common;
using SM.Core.Common.Enums.Collections;

namespace SM.Core.Features.Users.LikeHistory
{
	public class LikeHistoryRequest : IRequest<ApiResponse<LikeHistoryResponse>>
	{
		[FromRoute(Name ="userId")]
		public int UserId { get; set; }

        [FromQuery]
        public int PageIndex { get; set; }

        [FromQuery]
        public int PageSize { get; set; } = (int)DefaultPageSize.LikeHistory;
    }
}

