using System;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using SM.Core.Common;

namespace SM.Core.Features.Articles.CheckArticleLike
{
	public class CheckArticleLikeRequest : IRequest<ApiResponse<CheckArticleLikeResponse>>
	{
		[FromRoute(Name ="articleId")]
		public int ArticleId { get; set; }
	}
}

