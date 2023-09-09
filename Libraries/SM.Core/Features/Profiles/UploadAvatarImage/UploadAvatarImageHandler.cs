using MediatR;
using SM.Core.Common;
using SM.Core.Common.Enums.Media;
using SM.Core.DTOs.Media;
using SM.Core.Features.Profiles.UploadProfileImage;
using SM.Core.Interfaces.Services;
using SM.Core.Interfaces.Services.Media;
using SM.Core.Interfaces.Services.Membership;

namespace SM.Core.Features.Profiles.UploadAvatarImage
{
    public class UploadAvatarImageHandler : IRequestHandler<UploadAvatarImageRequest, ApiResponse<UploadAvatarImageResponse>>
    {
        private readonly IWorkContext _workContext;
        private readonly IUserService _userService;
        private readonly IFileService _fileService;

        public UploadAvatarImageHandler(IWorkContext workContext, IFileService fileService, IUserService userService)
        {
            _workContext = workContext;
            _fileService = fileService;
            _userService = userService;
        }

        public async Task<ApiResponse<UploadAvatarImageResponse>> Handle(UploadAvatarImageRequest request, CancellationToken cancellationToken)
        {
            FileInformationDTO fileInformationDTO = await _fileService.UploadAsync(request.File, RegisteredFileType.Avatars);
            await _userService.ChangeAvatarImageAsync(fileInformationDTO.FileName, await _workContext.GetAuthenticatedUserIdAsync());
            UploadAvatarImageResponse UploadAvatarImage = new UploadAvatarImageResponse { ProfileImage = fileInformationDTO };

            return ApiResponse<UploadAvatarImageResponse>.Successful(UploadAvatarImage, "Profile image added.");

        }
    }
}

