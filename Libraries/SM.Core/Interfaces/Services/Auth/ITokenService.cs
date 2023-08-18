using SM.Core.Domain;
using SM.Core.DTOs.Auth;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SM.Core.Interfaces.Services.Auth
{
    public interface ITokenService
    {
        TokenDTO CreateToken(ApplicationUser applicationUser);
    }
}
