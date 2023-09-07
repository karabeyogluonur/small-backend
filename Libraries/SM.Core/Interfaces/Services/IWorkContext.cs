using System;
using SM.Core.Domain;

namespace SM.Core.Interfaces.Services
{
	public interface IWorkContext
	{
        Task<ApplicationUser> GetAuthenticatedUserAsync();

        Task<int> GetAuthenticatedUserIdAsync();

    }
}

