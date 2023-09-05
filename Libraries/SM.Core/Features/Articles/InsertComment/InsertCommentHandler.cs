using System;
using AutoMapper;
using MediatR;
using SM.Core.Common;
using SM.Core.Domain;
using SM.Core.DTOs.Blog;
using SM.Core.Interfaces.Services.Auth;
using SM.Core.Interfaces.Services.Blog;

namespace SM.Core.Features.Articles.InsertComment
{
	public class InsertCommentHandler : IRequestHandler<InsertCommentRequest,ApiResponse<InsertCommentResponse>>
	{
        private readonly ICommentService _commentService;
        private readonly IAuthService _authService;
        private readonly IMapper _mapper;

        public InsertCommentHandler(ICommentService commentService, IMapper mapper, IAuthService authService)
        {
            _commentService = commentService;
            _mapper = mapper;
            _authService = authService;
        }

        public async Task<ApiResponse<InsertCommentResponse>> Handle(InsertCommentRequest request, CancellationToken cancellationToken)
        {
            ApplicationUser author = await _authService.GetAuthenticatedCustomerAsync();

            Comment comment = new Comment { Content = request.Content, ArticleId = request.ArticleId, AuthorId = author.Id };

            await _commentService.InsertCommentAsync(comment);

            InsertCommentResponse insertCommentResponse = new InsertCommentResponse { Comment = _mapper.Map<CommentDTO>(comment) };

            return ApiResponse<InsertCommentResponse>.Successful(null, "Comment successfully added");
        }
    }
}

