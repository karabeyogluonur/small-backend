using System;
using MediatR;
using SM.Core.Common;
using SM.Core.Interfaces.Services.Media;
using SM.Core.Common.Enums.Media;
using SM.Core.DTOs.Media;

namespace SM.Core.Features.Articles.UploadArticleImage
{
    public class UploadArticleImageHandler : IRequestHandler<UploadArticleImageRequest, ApiResponse<UploadArticleImageResponse>>
    {
        private readonly IFileService _fileService;

        public UploadArticleImageHandler(IFileService fileService)
        {
            _fileService = fileService;
        }

        public async Task<ApiResponse<UploadArticleImageResponse>> Handle(UploadArticleImageRequest request, CancellationToken cancellationToken)
        {
            FileInformationDTO fileInformationDTO = await _fileService.UploadAsync(formFile: request.File,registeredFileType:RegisteredFileType.Articles);

            UploadArticleImageResponse uploadArticleImageResponse = new UploadArticleImageResponse { FileInformation = fileInformationDTO };

            return ApiResponse<UploadArticleImageResponse>.Successful(uploadArticleImageResponse, "The article image has been successfully added.");
        }
    }
}

