using System;
using AutoMapper;
using MediatR;
using SM.Core.Common;
using SM.Core.Common.Enums.Blog;
using SM.Core.Domain;
using SM.Core.DTOs.Blog;
using SM.Core.Interfaces.Services;
using SM.Core.Interfaces.Services.Auth;
using SM.Core.Interfaces.Services.Blog;

namespace SM.Core.Features.Comments.InsertReply
{
    public class InsertReplyHandler : IRequestHandler<InsertReplyRequest, ApiResponse<InsertReplyResponse>>
    {
        private readonly ICommentService _commentService;
        private readonly IAuthService _authService;
        private readonly IWorkContext _workContext;
        private readonly IMapper _mapper;

        public InsertReplyHandler(ICommentService commentService, IAuthService authService, IMapper mapper, IWorkContext workContext)
        {
            _commentService = commentService;
            _authService = authService;
            _workContext = workContext;
            _mapper = mapper;
        }

        public async Task<ApiResponse<InsertReplyResponse>> Handle(InsertReplyRequest request, CancellationToken cancellationToken)
        {
            Comment comment = await _commentService.GetCommentByIdAsync(request.CommentId);

            if (comment == null)
                return ApiResponse<InsertReplyResponse>.Error(null, "Comment not found.");

            CommentReply commentReply = _mapper.Map<CommentReply>(request);

            commentReply.AuthorId = await _workContext.GetAuthenticatedUserIdAsync();

            await _commentService.InsertCommentReplyAsync(commentReply);

            await _commentService.CheckCommentRepliesAsync(comment.Id, CommentReplyOperation.Insert);

            InsertReplyResponse insertReplyResponse = new InsertReplyResponse { CommentReply = _mapper.Map<CommentReplyDTO>(commentReply) }; 

            return ApiResponse<InsertReplyResponse>.Successful(insertReplyResponse,"Comment reply successully added.");

            
        }
    }
}

