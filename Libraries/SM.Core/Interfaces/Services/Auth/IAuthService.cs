using SM.Core.DTOs.Auth;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SM.Core.Interfaces.Services.Auth
{
    public interface IAuthService
    {
        Task<TokenDTO> SignInAsync(string email, string password);
    }
}
