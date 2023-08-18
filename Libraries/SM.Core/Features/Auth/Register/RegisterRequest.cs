using MediatR;
using SM.Core.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SM.Core.Features.Auth.Register
{
    public class RegisterRequest : IRequest<ApiResponse<RegisterResponse>>
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
    }
}
