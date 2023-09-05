using System;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using SM.Core.Common;

namespace SM.Core.Features.Comments.DeleteComment
{
	public class DeleteCommentRequest : IRequest<ApiResponse<DeleteCommentResponse>>
	{
		[FromRoute]
		public int CommentId { get; set; }
	}
}

