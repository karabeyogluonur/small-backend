using System;
using MediatR;
using SM.Core.Common;
using SM.Core.Common.Enums.Collections;

namespace SM.Core.Features.Articles.GetAllArticle
{
	public class GetAllArticleRequest : IRequest<ApiResponse<GetAllArticleResponse>>
	{
		public string? TopicIds { get; set; }
		public int PageIndex { get; set; }
		public int PageSize { get; set; } = (int)DefaultPageSize.GetAllArticle;
	}
}

