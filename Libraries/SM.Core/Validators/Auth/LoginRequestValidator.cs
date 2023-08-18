using FluentValidation;
using SM.Core.Features.Auth.Login;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SM.Core.Validators.Auth
{
    public class LoginRequestValidator : AbstractValidator<LoginRequest>
    {
        public LoginRequestValidator()
        {
            RuleFor(login => login.Email).EmailAddress().NotNull().NotEmpty();
            RuleFor(login => login.Password).NotNull().NotEmpty();

        }
    }
}
