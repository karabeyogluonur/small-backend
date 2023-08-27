using System;
using FluentValidation;
using SM.Core.Common.Helpers;
using SM.Core.Features.Articles.GetAllArticle;
using static System.Net.Mime.MediaTypeNames;

namespace SM.Core.Validators.Blog
{
	public class GetAllArticleRequestValidator : AbstractValidator<GetAllArticleRequest>
	{
		public GetAllArticleRequestValidator()
		{
            When(request=>request.TopicIds != null, () =>
            {
                RuleFor(request=>request.TopicIds).Must(BeValidIdFormat).WithMessage("Invalid ID format.");
            });
        }

        private bool BeValidIdFormat(string ids)
        {
            return ParserHelper.ParseStringIdValidation(ids);
        }
    }
}

