using System;
using System.Security.Claims;
using Castle.Core.Resource;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using SM.Core.Domain;
using SM.Core.Interfaces.Services;

namespace SM.Infrastructre.Services
{
    public class WorkContext : IWorkContext
	{
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly UserManager<ApplicationUser> _userManager;


        protected ApplicationUser _cachedUser;
        protected int _cachedUserId;

        public WorkContext(IHttpContextAccessor httpContextAccessor, UserManager<ApplicationUser> userManager)
        {
            _httpContextAccessor = httpContextAccessor;
            _userManager = userManager;
        }

        public async Task<ApplicationUser> GetAuthenticatedUserAsync()
        {
            if (_cachedUser != null)
                return _cachedUser;

            await SetAuthenticatedUserAsync();

            return _cachedUser;

        }

        public async Task<int> GetAuthenticatedUserIdAsync()
        {
            if (_cachedUserId != null && _cachedUserId != 0)
                return _cachedUserId;

            AuthenticateResult authenticateResult = await _httpContextAccessor.HttpContext.AuthenticateAsync(JwtBearerDefaults.AuthenticationScheme);

            if (!authenticateResult.Succeeded)
                throw new UnauthorizedAccessException();

            Claim? claim = authenticateResult.Principal.FindFirst(claim => claim.Type == ClaimTypes.NameIdentifier);

            if (claim == null)
                throw new UnauthorizedAccessException();

            _cachedUserId = Convert.ToInt32(claim.Value);

            return _cachedUserId;

        }

        private async Task SetAuthenticatedUserAsync()
        {
            AuthenticateResult authenticateResult = await _httpContextAccessor.HttpContext.AuthenticateAsync(JwtBearerDefaults.AuthenticationScheme);

            if (!authenticateResult.Succeeded)
                throw new UnauthorizedAccessException();

            Claim? claim = authenticateResult.Principal.FindFirst(claim => claim.Type == ClaimTypes.Name);

            if (claim == null)
                throw new UnauthorizedAccessException();

            ApplicationUser applicationUser =  await _userManager.FindByEmailAsync(claim.Value);

            if(applicationUser == null)
                throw new UnauthorizedAccessException();

            _cachedUser = applicationUser;
            _cachedUserId = applicationUser.Id;
        }
    }
}

