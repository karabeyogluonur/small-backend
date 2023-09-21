using System;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using SM.Core.Common;

namespace SM.Core.Features.Articles.LikeArticle
{
	public class LikeArticleRequest : IRequest<ApiResponse<LikeArticleResponse>>
	{
		[FromRoute(Name = "articleId")]
		public int ArticleId { get; set; }
	}
}

