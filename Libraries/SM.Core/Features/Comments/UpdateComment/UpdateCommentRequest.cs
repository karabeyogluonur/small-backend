using System;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using SM.Core.Common;

namespace SM.Core.Features.Comments.UpdateComment
{
	public class UpdateCommentRequest : IRequest<ApiResponse<UpdateCommentResponse>>
	{
		[FromRoute]
		public int CommentId { get; set; }
		[FromBody]
		public string Content { get; set; }
	}
}

