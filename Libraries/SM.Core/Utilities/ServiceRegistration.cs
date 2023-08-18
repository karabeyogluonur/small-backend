using Microsoft.Extensions.DependencyInjection;
using SM.Core.Mappers.AutoMapper;

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
        }
    }
}
