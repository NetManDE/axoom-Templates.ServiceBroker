using System;
using Axoom.Extensions.Prometheus.Standalone;
using JetBrains.Annotations;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MyVendor.ServiceBroker.Broker;
using MyVendor.ServiceBroker.Infrastructure;

namespace MyVendor.ServiceBroker
{
    /// <summary>
    /// Configures dependency injection.
    /// </summary>
    [UsedImplicitly]
    public class Startup : IStartup
    {
        public IConfiguration Configuration { get; }

        /// <summary>
        /// Called by ASP.NET Core to set up an environment.
        /// </summary>
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        /// <summary>
        /// Called by ASP.NET Core to register services.
        /// </summary>
        public IServiceProvider ConfigureServices(IServiceCollection services)
        {
            services.AddPrometheusServer(Configuration.GetSection("Metrics"))
                    .AddSecurity(Configuration.GetSection("Authentication"))
                    .AddRestApi()
                    .AddBroker(Configuration.GetSection("Broker"));

            services.AddHealthChecks();

            return services.BuildServiceProvider();
        }

        /// <summary>
        /// Called by ASP.NET Core to configure services after they have been registered.
        /// </summary>
        public void Configure(IApplicationBuilder app)
            => app.UseHealthChecks("/health")
                  .UseRestApi();
    }
}
