using MediatR;
using SM.Core.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SM.Core.Features.Auth.ResetPassword
{
    public class ResetPasswordRequest : IRequest<ApiResponse<ResetPasswordResponse>>
    {
        public string Password { get; set; }
        public string ResetPasswordToken { get; set; }
    }
}
