using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using SM.Core.Common.Behaviors;
using SM.Core.Mappers.AutoMapper;
using SM.Core.Validators.Auth;

namespace SM.Core.Utilities
{
    public static class ServiceRegistration
    {
        public static void AddCoreServices(this IServiceCollection services)
        {
            #region MediatR

            services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(ServiceRegistration).Assembly));

            #endregion

            #region Automapper

            services.AddAutoMapper(typeof(AutoMapperProfile).Assembly);

            #endregion

            #region Validation

            services.AddControllers(options =>
            {
                options.Filters.Add<ValidationBehavior>();
            });

            services.AddFluentValidationAutoValidation();
            services.AddValidatorsFromAssemblyContaining<LoginRequestValidator>();

            services.Configure<ApiBehaviorOptions>(options =>
            {
                options.SuppressModelStateInvalidFilter = true;
            });

            #endregion
        }
    }
}
