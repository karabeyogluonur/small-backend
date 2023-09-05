using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SM.Core.Common;
using SM.Core.Features.Comments.DeleteComment;
using SM.Core.Features.Comments.DeleteReply;
using SM.Core.Features.Comments.GetReply;
using SM.Core.Features.Comments.InsertReply;
using SM.Core.Features.Comments.UpdateComment;

namespace SM.API.Controllers
{
    [Route("api/comments")]
    public class CommentController : Controller
    {
        private readonly IMediator _mediator;
        public CommentController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPut("{commentId}")]
        [Authorize]
        public async Task<IActionResult> Update(UpdateCommentRequest updateCommentRequest)
        {
            ApiResponse<UpdateCommentResponse> apiResponse = await _mediator.Send(updateCommentRequest);

            if (apiResponse.Success)
                return Ok(apiResponse);
            else
                return BadRequest(apiResponse);
        }

        [HttpDelete("{commentId}")]
        [Authorize]
        public async Task<IActionResult> Delete(DeleteCommentRequest deleteCommentRequest)
        {
            ApiResponse<DeleteCommentResponse> apiResponse = await _mediator.Send(deleteCommentRequest);

            if (apiResponse.Success)
                return Ok(apiResponse);
            else
                return BadRequest(apiResponse);
        }

        [HttpPost("{commentId}/replies")]
        [Authorize]
        public async Task<IActionResult> InsertReply([FromBody] InsertReplyRequest insertReplyRequest, [FromRoute] int commentId )
        {
            insertReplyRequest.CommentId = commentId;

            ApiResponse<InsertReplyResponse> apiResponse = await _mediator.Send(insertReplyRequest);
            if (apiResponse.Success)
                return Ok(apiResponse);
            else
                return BadRequest(apiResponse);
        }

        [HttpDelete("replies/{commentReplyId}")]
        [Authorize]
        public async Task<IActionResult> DeleteReply([FromRoute] DeleteReplyRequest deleteReplyRequest)
        {
            ApiResponse<DeleteReplyResponse> apiResponse = await _mediator.Send(deleteReplyRequest);
            if (apiResponse.Success)
                return Ok(apiResponse);
            else
                return BadRequest(apiResponse);
        }

        [HttpGet("{commentId}/replies")]
        public async Task<IActionResult> GetReplies(GetReplyRequest getReplyRequest)
        {
            ApiResponse<GetReplyResponse> apiResponse = await _mediator.Send(getReplyRequest);
            if (apiResponse.Success)
                return Ok(apiResponse);
            else
                return BadRequest(apiResponse);
        }


    }
}

