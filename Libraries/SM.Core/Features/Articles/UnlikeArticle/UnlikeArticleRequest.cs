using System;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using SM.Core.Common;

namespace SM.Core.Features.Articles.UnlikeArticle
{
	public class UnlikeArticleRequest : IRequest<ApiResponse<UnlikeArticleResponse>>
    {
		[FromRoute(Name = "articleId")]
		public int ArticleId { get; set; }
	}
}

