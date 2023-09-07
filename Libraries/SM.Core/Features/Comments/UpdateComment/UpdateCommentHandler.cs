using System;
using AutoMapper;
using MediatR;
using SM.Core.Common;
using SM.Core.Domain;
using SM.Core.Interfaces.Services;
using SM.Core.Interfaces.Services.Auth;
using SM.Core.Interfaces.Services.Blog;

namespace SM.Core.Features.Comments.UpdateComment
{
    public class UpdateCommentHandler : IRequestHandler<UpdateCommentRequest, ApiResponse<UpdateCommentResponse>>
    {
        private readonly ICommentService _commentService;
        private readonly IAuthService _authService;
        private readonly IWorkContext _workContext;
        private readonly IMapper _mapper;

        public UpdateCommentHandler(ICommentService commentService, IAuthService authService, IMapper mapper, IWorkContext workContext)
        {
            _commentService = commentService;
            _authService = authService;
            _mapper = mapper;
            _workContext = workContext;
        }

        public async Task<ApiResponse<UpdateCommentResponse>> Handle(UpdateCommentRequest request, CancellationToken cancellationToken)
        {
            Comment comment = await _commentService.GetCommentByIdAsync(request.CommentId);

            if (comment == null)
                return ApiResponse<UpdateCommentResponse>.Error(null, "Comment not found.");

            if (comment.AuthorId != await _workContext.GetAuthenticatedUserIdAsync())
                throw new UnauthorizedAccessException("You are not authorized for this article.");

            comment.Content = request.Content;
            _commentService.UpdateComment(comment);

            return ApiResponse<UpdateCommentResponse>.Error(null, "Comment successfully updated.");
        }
    }
}

