using System;
using MediatR;
using Microsoft.AspNetCore.Http;
using SM.Core.Common;

namespace SM.Core.Features.Profiles.UploadProfileImage
{
	public class UploadAvatarImageRequest : IRequest<ApiResponse<UploadAvatarImageResponse>>
	{
		public IFormFile File { get; set; }
	}
}

