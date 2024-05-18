using MediatR;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace BGMaterial.Application.Services
{
    public static class ServiceRegistiration
    {
        public static void AddApplicationService(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddMediatR(config => config.RegisterServicesFromAssembly(typeof(ServiceRegistiration).Assembly));
            services.AddAutoMapper(Assembly.GetExecutingAssembly());
        }
    }
}
