using System;
using AutoMapper;
using MediatR;
using SM.Core.Common;
using SM.Core.Domain;
using SM.Core.Features.Articles.GetAllArticle;
using SM.Core.Interfaces.Collections;
using SM.Core.Interfaces.Services.Blog;

namespace SM.Core.Features.Articles.GetComment
{
    public class GetCommentHandler : IRequestHandler<GetCommentRequest, ApiResponse<GetCommentResponse>>
    {
        private readonly ICommentService _commentService;
        private readonly IMapper _mapper;

        public GetCommentHandler(ICommentService commentService, IMapper mapper)
        {
            _commentService = commentService;
            _mapper = mapper;
        }

        public async Task<ApiResponse<GetCommentResponse>> Handle(GetCommentRequest request, CancellationToken cancellationToken)
        {
            IPagedList<Comment> comments = await _commentService.GetAllCommentsAsync(request.ArticleId, request.PageIndex, request.PageSize, includeReplies: false);
            GetCommentResponse getCommentResponse = _mapper.Map<GetCommentResponse>(comments);
            return ApiResponse<GetCommentResponse>.Successful(getCommentResponse, "Article receive successful.");
        }
    }
}

