using MediatR;
using SM.Core.Common;

namespace SM.Core.Features.Auth.RefreshPasswordConfirmation
{
    public class ResetPasswordConfirmationRequest : IRequest<ApiResponse<ResetPasswordConfirmationResponse>>
    {
        public string ResetPasswordToken { get; set; }
    }
}
