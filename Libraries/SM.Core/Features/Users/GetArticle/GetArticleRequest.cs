using System;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using SM.Core.Common;
using SM.Core.Common.Enums.Collections;

namespace SM.Core.Features.Users.GetArticle
{
	public class GetArticleRequest : IRequest<ApiResponse<GetArticleResponse>>
	{
        [FromRoute(Name ="userId")]
        public int UserId { get; set; }
        [FromQuery]
        public int PageIndex { get; set; }
        [FromQuery]
        public int PageSize { get; set; } = (int)DefaultPageSize.GetArticle;
    }
}

