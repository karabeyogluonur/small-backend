﻿using SM.Core.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SM.Core.Interfaces.Services.Membership
{
    public interface IUserService
    {
        Task UpdateRefreshTokenAsync(ApplicationUser applicationUser, string refreshToken);
    }
}