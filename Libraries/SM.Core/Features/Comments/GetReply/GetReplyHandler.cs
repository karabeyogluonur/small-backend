using System;
using AutoMapper;
using MediatR;
using SM.Core.Common;
using SM.Core.Domain;
using SM.Core.Features.Articles.GetComment;
using SM.Core.Interfaces.Collections;
using SM.Core.Interfaces.Services.Blog;

namespace SM.Core.Features.Comments.GetReply
{
    public class GetReplyHandler : IRequestHandler<GetReplyRequest, ApiResponse<GetReplyResponse>>
    {
        private readonly ICommentService _commentService;
        private readonly IMapper _mapper;

        public GetReplyHandler(ICommentService commentService, IMapper mapper)
        {
            _commentService = commentService;
            _mapper = mapper;
        }

        public async Task<ApiResponse<GetReplyResponse>> Handle(GetReplyRequest request, CancellationToken cancellationToken)
        {
            IPagedList<CommentReply> commentReplies = await _commentService.GetAllCommentRepliesAsync(request.CommentId, request.PageIndex, request.PageSize);
            GetReplyResponse getReplyResponse = _mapper.Map<GetReplyResponse>(commentReplies);
            return ApiResponse<GetReplyResponse>.Successful(getReplyResponse, "Comment replies receive successful.");
        }
    }
}

