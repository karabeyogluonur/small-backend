using MediatR;
using SM.Core.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SM.Core.Features.Auth.ForgotPassword
{
    public class ForgotPasswordRequest : IRequest<ApiResponse<ForgotPasswordResponse>>
    {
        public string Email { get; set; }
    }
}
