using System;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using SM.Core.Common;

namespace SM.Core.Features.Users.RecentSearch
{
	public class RecentSearchRequest : IRequest<ApiResponse<RecentSearchResponse>>
	{
		[FromRoute(Name ="userId")]
		public int UserId { get; set; }
	}
}

