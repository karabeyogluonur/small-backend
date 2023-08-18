using Microsoft.Extensions.DependencyInjection;

namespace SM.Core.Utilities
{
    public static class ServiceRegistration
    {
        public static void AddCoreServices(this IServiceCollection services)
        {
            #region MediatR

            services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(ServiceRegistration).Assembly));

            #endregion 
        }
    }
}
