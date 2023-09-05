using System;
using MediatR;
using SM.Core.Common;
using SM.Core.Domain;
using SM.Core.Interfaces.Services.Auth;
using SM.Core.Interfaces.Services.Blog;

namespace SM.Core.Features.Comments.DeleteComment
{
    public class DeleteCommentHandler : IRequestHandler<DeleteCommentRequest, ApiResponse<DeleteCommentResponse>>
    {
        private readonly ICommentService _commentService;
        private readonly IAuthService _authService;

        public DeleteCommentHandler(ICommentService commentService, IAuthService authService)
        {
            _commentService = commentService;
            _authService = authService;
        }

        public async Task<ApiResponse<DeleteCommentResponse>> Handle(DeleteCommentRequest request, CancellationToken cancellationToken)
        {
            Comment comment = await _commentService.GetCommentByIdAsync(request.CommentId);

            if (comment == null)
                return ApiResponse<DeleteCommentResponse>.Error(null, "Comment not found!");

            ApplicationUser author = await _authService.GetAuthenticatedCustomerAsync();

            if(comment.AuthorId != author.Id)
                throw new UnauthorizedAccessException("You are not authorized for this comment.");

            _commentService.DeleteComment(comment);

            return ApiResponse<DeleteCommentResponse>.Successful(null, "Comment successfully deleted.");

        }
    }
}

