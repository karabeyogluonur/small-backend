using System;
using System.Xml.Linq;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using SM.Core.Common;
using SM.Core.Common.Enums.Collections;

namespace SM.Core.Features.Profiles.GetDraft
{
	public class GetDraftRequest : IRequest<ApiResponse<GetDraftResponse>>
	{
        [FromQuery]
        public int PageIndex { get; set; }
        [FromQuery]
        public int PageSize { get; set; } = (int)DefaultPageSize.GetDraft;
    }
}

