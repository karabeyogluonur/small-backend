using System;
using MediatR;
using SM.Core.Common;
using SM.Core.Common.Enums.Blog;
using SM.Core.Domain;
using SM.Core.Features.Comments.DeleteComment;
using SM.Core.Interfaces.Services.Auth;
using SM.Core.Interfaces.Services.Blog;

namespace SM.Core.Features.Comments.DeleteReply
{
    public class DeleteReplyHandler : IRequestHandler<DeleteReplyRequest, ApiResponse<DeleteReplyResponse>>
    {
        private readonly IAuthService _authService;
        private readonly ICommentService _commentService;

        public DeleteReplyHandler(IAuthService authService, ICommentService commentService)
        {
            _authService = authService;
            _commentService = commentService;
        }

        public async Task<ApiResponse<DeleteReplyResponse>> Handle(DeleteReplyRequest request, CancellationToken cancellationToken)
        {
            CommentReply commentReply = await _commentService.GetCommentReplyByIdAsync(request.CommentReplyId);

            if (commentReply == null)
                return ApiResponse<DeleteReplyResponse>.Error(null, "Comment reply is not found!");

            ApplicationUser author = await _authService.GetAuthenticatedCustomerAsync();

            if(commentReply.AuthorId != author.Id)
                throw new UnauthorizedAccessException("You are not authorized for this comment reply.");

            _commentService.DeleteCommentReply(commentReply);

            await _commentService.CheckCommentRepliesAsync(commentReply.CommentId,CommentReplyOperation.Delete);

            return ApiResponse<DeleteReplyResponse>.Successful(null, "Comment reply successfully deleted.");
        }
    }
}

