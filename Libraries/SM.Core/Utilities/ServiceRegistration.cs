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

            services.AddAutoMapper(typeof(AutoMapperProfile));

            #endregion

            #region Validation

            services.AddFluentValidation(configuration => configuration.RegisterValidatorsFromAssemblyContaining<LoginRequestValidator>())
                .Configure<ApiBehaviorOptions>(options => options.SuppressModelStateInvalidFilter = true);

            services.AddControllers(options => options.Filters.Add<ValidationBehavior>());

            #endregion
        }
    }
}
