using System;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using OpenServiceBroker.Instances;

namespace MyVendor.ServiceBroker.Broker
{
    public class ServiceInstanceService : IServiceInstanceBlocking
    {
        private readonly IServiceInstanceMetrics _metrics;
        private readonly ILogger<ServiceInstanceService> _logger;

        public ServiceInstanceService(IServiceInstanceMetrics metrics, ILogger<ServiceInstanceService> logger)
        {
            _metrics = metrics;
            _logger = logger;
        }

        public Task<ServiceInstanceProvision> ProvisionAsync(ServiceInstanceContext context, ServiceInstanceProvisionRequest request)
        {
            _logger.LogInformation("Provisioning instance {0} as service {1}.", context.InstanceId, request.ServiceId);
            _metrics.Provisioned(request.ServiceId);
            return Task.FromResult(new ServiceInstanceProvision());
        }

        public Task UpdateAsync(ServiceInstanceContext context, ServiceInstanceUpdateRequest request)
        {
            _logger.LogInformation("Updating instance {0} as service {1}.", context.InstanceId, request.ServiceId);
            _metrics.Updated(request.ServiceId);
            return Task.CompletedTask;
        }

        public Task DeprovisionAsync(ServiceInstanceContext context, string serviceId, string planId)
        {
            _logger.LogInformation("Deprovisioning instance {0}.", context.InstanceId);
            _metrics.Deprovisioned();
            return Task.CompletedTask;
        }

        public Task<ServiceInstanceResource> FetchAsync(string instanceId)
        {
            throw new NotImplementedException();
        }
    }
}
