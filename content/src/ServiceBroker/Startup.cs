using Axoom.Extensions.Prometheus.Standalone;
using JetBrains.Annotations;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MyVendor.ServiceBroker.Broker;
using MyVendor.ServiceBroker.Infrastructure;

namespace MyVendor.ServiceBroker
{
    [UsedImplicitly]
    public class Startup
    {
        private readonly IConfiguration _configuration;

        public Startup(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        // Register services for DI
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddPrometheusServer(_configuration.GetSection("Metrics"))
                    .AddSecurity(_configuration.GetSection("Authentication"))
                    .AddRestApi()
                    .AddDatabase(_configuration.GetConnectionString("Database"));

            services.AddHealthChecks()
                    .AddDbContextCheck<DbContext>();

            services.AddBroker(_configuration.GetSection("Broker"));
        }

        // Configure HTTP request pipeline
        public void Configure(IApplicationBuilder app)
            => app.UseHealthChecks("/health")
                  .UseRestApi();
    }
}
