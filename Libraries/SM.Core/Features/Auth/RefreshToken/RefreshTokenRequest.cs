using MediatR;
using SM.Core.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SM.Core.Features.Auth.RefreshToken
{
    public class RefreshTokenRequest : IRequest<ApiResponse<RefreshTokenResponse>>
    {
        public string? RefreshToken { get; set; }
    }
}
