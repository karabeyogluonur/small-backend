using FluentValidation;
using SM.Core.Features.Auth.Register;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SM.Core.Validators.Auth
{
    public class RegisterRequestValidator : AbstractValidator<RegisterRequest>
    {
        public RegisterRequestValidator()
        {
            RuleFor(register => register.FirstName).NotEmpty().NotNull();
            RuleFor(register => register.LastName).NotEmpty().NotNull();
            RuleFor(register => register.Email).NotEmpty().NotNull().EmailAddress();
            RuleFor(register => register.Password).NotEmpty().NotNull();
            RuleFor(register => register.UserName).NotEmpty().NotNull();

        }
    }
}
