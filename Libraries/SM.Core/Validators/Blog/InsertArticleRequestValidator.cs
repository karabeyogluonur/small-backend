using System;
using FluentValidation;
using SM.Core.Features.Articles.InsertArticle;

namespace SM.Core.Validators.Blog
{
	public class InsertArticleRequestValidator : AbstractValidator<InsertArticleRequest>
	{
		public InsertArticleRequestValidator()
		{
            When(request => request.Title == null || request.Title == "", () =>
            {
                RuleFor(request => request.Content).NotNull().NotEmpty();
            });
            When(request => request.Content == null || request.Content == "", () =>
            {
                RuleFor(request => request.Title).NotNull().NotEmpty();
            });
        }
	}
}

