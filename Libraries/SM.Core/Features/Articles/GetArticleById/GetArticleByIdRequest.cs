using System;
using MediatR;
using SM.Core.Common;

namespace SM.Core.Features.Articles.GetArticleById
{
	public class GetArticleByIdRequest : IRequest<ApiResponse<GetArticleByIdResponse>>
	{
		public int articleId { get; set; }
	}
}

