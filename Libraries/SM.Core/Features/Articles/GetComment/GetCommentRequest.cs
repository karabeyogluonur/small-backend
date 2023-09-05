using System;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using SM.Core.Common;
using SM.Core.Common.Enums.Collections;

namespace SM.Core.Features.Articles.GetComment
{
	public class GetCommentRequest : IRequest<ApiResponse<GetCommentResponse>>
	{
		[FromRoute]
		public int ArticleId { get; set; }

		[FromQuery]
		public int PageIndex { get; set; }
        [FromQuery]
        public int PageSize { get; set; } = (int)DefaultPageSize.GetComment;
    }
}

