using System;
using MediatR;
using Microsoft.AspNetCore.Http;
using SM.Core.Common;

namespace SM.Core.Features.Articles.UploadArticleImage
{
	public class UploadArticleImageRequest : IRequest<ApiResponse<UploadArticleImageResponse>>
	{
        public IFormFile File { get; set; }
    }
}

