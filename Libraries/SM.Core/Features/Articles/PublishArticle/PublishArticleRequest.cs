using System;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using SM.Core.Common;

namespace SM.Core.Features.Articles.PublishArticle
{
	public class PublishArticleRequest : IRequest<ApiResponse<PublishArticleResponse>>
	{
		public int ArticleId { get; set; }
	}
}

