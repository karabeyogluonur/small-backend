﻿using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using SM.Core.Common.Exceptions;
using SM.Core.Domain;
using SM.Core.DTOs.Auth;
using SM.Core.Features.Auth.Register;
using SM.Core.Interfaces.Services.Auth;
using SM.Core.Interfaces.Services.Membership;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SM.Infrastructre.Services.Auth
{
    public class AuthService : IAuthService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly ITokenService _tokenService;
        private readonly IUserService _userService;

        public AuthService(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, ITokenService tokenService, IUserService userService)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _tokenService = tokenService;
            _userService = userService;
        }

        public async Task<TokenDTO> RefreshTokenSignInAsync(string refreshToken)
        {
            ApplicationUser applicationUser = await _userManager.Users.FirstOrDefaultAsync(user => user.RefreshToken == refreshToken);

            if (applicationUser == null)
                throw new UserNotFoundException();

            TokenDTO tokenDTO = _tokenService.CreateToken(applicationUser);
            await _userService.UpdateRefreshTokenAsync(applicationUser, tokenDTO.RefreshToken);
            return tokenDTO;
        }

        public async Task<IdentityResult> RegisterAsync(ApplicationUser applicationUser, string password)
        {
            applicationUser.UserName = applicationUser.Email;
            return await _userManager.CreateAsync(applicationUser, password);
        }

        public async Task<TokenDTO> SignInAsync(string email, string password)
        {
            ApplicationUser applicationUser = await _userManager.FindByEmailAsync(email);

            if (applicationUser == null)
                throw new UserNotFoundException();

            SignInResult result = await _signInManager.CheckPasswordSignInAsync(applicationUser, password, false);

            if (!result.Succeeded)
                throw new AuthenticationErrorException();

            TokenDTO tokenDTO = _tokenService.CreateToken(applicationUser);
            await _userService.UpdateRefreshTokenAsync(applicationUser, tokenDTO.RefreshToken);

            return tokenDTO;
        }
    }
}
