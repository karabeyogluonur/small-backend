using System;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using SM.Core.Common;

namespace SM.Core.Features.Comments.InsertReply
{
    public class InsertReplyRequest : IRequest<ApiResponse<InsertReplyResponse>>
	{
		public int CommentId { get; set; }
        public string Content { get; set; }
	}
}

