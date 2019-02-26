using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using OpenServiceBroker;
using OpenServiceBroker.Catalogs;
using OpenServiceBroker.Instances;

namespace MyVendor.ServiceBroker.Broker
{
    public static class Startup
    {
        public static IServiceCollection AddBroker(this IServiceCollection services, IConfiguration configuration)
            => services.Configure<BrokerOptions>(configuration)
                       .AddSingleton<IServiceInstanceMetrics, ServiceInstanceMetrics>()
                       .AddTransient<ICatalogService, CatalogService>()
                       .AddTransient<IServiceInstanceBlocking, ServiceInstanceService>()
                       .AddOpenServiceBroker();
    }
}
