using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SM.Core.Common;
using SM.Core.Features.Articles.GetAllArticle;
using SM.Core.Features.Articles.GetArticleById;
using SM.Core.Features.Articles.GetComment;
using SM.Core.Features.Articles.InsertArticle;
using SM.Core.Features.Articles.InsertComment;
using SM.Core.Features.Articles.PublishArticle;
using SM.Core.Features.Articles.UpdateArticle;
using SM.Core.Features.Articles.UpdateTopic;
using SM.Core.Features.Articles.UploadArticleImage;

namespace SM.API.Controllers
{
    [Route("api/articles")]
    public class ArticleController : Controller
    {
        private readonly IMediator _mediator;
        public ArticleController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] GetAllArticleRequest getAllArticleRequest)
        {
            ApiResponse<GetAllArticleResponse> apiResponse = await _mediator.Send(getAllArticleRequest);
            return Ok(apiResponse);
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Insert([FromBody] InsertArticleRequest insertArticleRequest)
        {
            ApiResponse<InsertArticleResponse> apiResponse = await _mediator.Send(insertArticleRequest);
            return Ok(apiResponse);
        }

        [HttpPost("upload-article-image")]
        [Authorize]
        public async Task<IActionResult> UploadArticleImage([FromForm] UploadArticleImageRequest uploadArticleImageRequest)
        {
            ApiResponse<UploadArticleImageResponse> apiResponse = await _mediator.Send(uploadArticleImageRequest);
            return Ok(apiResponse);
        }

        [HttpPut]
        [Authorize]
        public async Task<IActionResult> Update([FromBody] UpdateArticleRequest updateArticleRequest)
        {
            ApiResponse<UpdateArticleResponse> apiResponse = await _mediator.Send(updateArticleRequest);

            if (apiResponse.Success)
                return Ok(apiResponse);
            else
                return BadRequest(apiResponse);
        }

        [HttpGet("{id}")]
        [Authorize]
        public async Task<IActionResult> GetById([FromRoute] int id)
        {
            ApiResponse<GetArticleByIdResponse> apiResponse = await _mediator.Send(new GetArticleByIdRequest() { articleId = id });

            if (apiResponse.Success)
                return Ok(apiResponse);
            else
                return BadRequest(apiResponse);
        }

        [HttpPost("UpdateTopics")]
        [Authorize]
        public async Task<IActionResult> UpdateTopics([FromBody] UpdateTopicRequest updateTopicRequest)
        {
            ApiResponse<UpdateTopicResponse> apiResponse = await _mediator.Send(updateTopicRequest);

            if (apiResponse.Success)
                return Ok(apiResponse);
            else
                return BadRequest(apiResponse);
        }

        [HttpPost("{id}/publish")]
        [Authorize]
        public async Task<IActionResult> Publish([FromRoute] int id)
        {
            ApiResponse<PublishArticleResponse> apiResponse = await _mediator.Send(new PublishArticleRequest { ArticleId = id });

            if (apiResponse.Success)
                return Ok(apiResponse);
            else
                return BadRequest(apiResponse);
        }

        #region Comments

        [HttpPost("{articleId}/comments")]
        [Authorize]
        public async Task<IActionResult> InsertComment([FromBody] InsertCommentRequest insertCommentRequest, [FromRoute] int articleId)
        {
            insertCommentRequest.ArticleId = articleId;
            ApiResponse<InsertCommentResponse> apiResponse = await _mediator.Send(insertCommentRequest);

            if (apiResponse.Success)
                return Ok(apiResponse);
            else
                return BadRequest(apiResponse);
        }

        [HttpGet("{articleId}/comments")]
        public async Task<IActionResult> GetComments(GetCommentRequest getCommentRequest)
        {
            ApiResponse<GetCommentResponse> apiResponse = await _mediator.Send(getCommentRequest);

            if (apiResponse.Success)
                return Ok(apiResponse);
            else
                return BadRequest(apiResponse);
        }
        #endregion

    }
}

