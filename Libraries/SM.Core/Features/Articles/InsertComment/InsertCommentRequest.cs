using MediatR;
using SM.Core.Common;

namespace SM.Core.Features.Articles.InsertComment
{
    public class InsertCommentRequest : IRequest<ApiResponse<InsertCommentResponse>>
	{
		public int ArticleId { get; set; }
		public string Content { get; set; }
	}
}

