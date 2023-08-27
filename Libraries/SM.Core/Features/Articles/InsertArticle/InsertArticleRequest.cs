using System;
using MediatR;
using SM.Core.Common;
using SM.Core.Domain;

namespace SM.Core.Features.Articles.InsertArticle
{
	public class InsertArticleRequest : IRequest<ApiResponse<InsertArticleResponse>>
	{
        public string? Title { get; set; }
        public string? Content { get; set; }
    }
}

