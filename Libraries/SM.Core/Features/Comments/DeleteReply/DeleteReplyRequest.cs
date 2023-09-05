using System;
using MediatR;
using SM.Core.Common;

namespace SM.Core.Features.Comments.DeleteReply
{
	public class DeleteReplyRequest : IRequest<ApiResponse<DeleteReplyResponse>>
	{
		public int CommentReplyId { get; set; }
	}
}

