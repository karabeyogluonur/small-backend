using MediatR;
using SM.Core.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SM.Core.Features.Auth.Login
{
    public class LoginRequest : IRequest<ApiResponse<LoginResponse>>
    {
        public string Email { get; set; }
        public string Password { get; set; }
    }
}
