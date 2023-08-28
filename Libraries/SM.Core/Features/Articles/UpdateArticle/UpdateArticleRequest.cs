using System;
using MediatR;
using SM.Core.Common;

namespace SM.Core.Features.Articles.UpdateArticle
{
	public class UpdateArticleRequest : IRequest<ApiResponse<UpdateArticleResponse>>
	{
		public int ArticleId { get; set; }
		public string Title { get; set; }
		public string Content { get; set; }
		public bool Published { get; set; }

	}
}

