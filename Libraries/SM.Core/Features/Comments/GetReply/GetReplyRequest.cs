using System;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using SM.Core.Common;
using SM.Core.Common.Enums.Collections;

namespace SM.Core.Features.Comments.GetReply
{
	public class GetReplyRequest : IRequest<ApiResponse<GetReplyResponse>>
	{
        [FromRoute]
        public int CommentId { get; set; }

        [FromQuery]
        public int PageIndex { get; set; }
        [FromQuery]
        public int PageSize { get; set; } = (int)DefaultPageSize.GetReply;
    }
}

